using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using API.DTO;
using API.DTO.User;
using API.Filters;
using Domain;
using Domain.ValueObjects.User;
using Metadata.Services.UserMetadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.User;

namespace API.Controllers {
    [Route("api/{v:apiVersion}/user")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase {
        private readonly IUserService _service;
        private readonly ILogger _logger;

        public UserController(IUserService service, ILogger<UserController> logger) {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<User> Index() {
            return _service.GetUsers();
        }
        
        [HttpPost("authenticate")]
        [AllowAnonymous, ValidationErrorFilter]
        public IActionResult LoginUser([FromBody] LoginRequestDTO body) {
            var data = _service.LoginUser(
                new Email(body.email),
                new Password(body.password)
            );
            
            if (!data.Success) {
                return NotFound(UserAuthTokensDTO.NotFound);
            }

            return Ok(new UserAuthTokensDTO(
                data.UserId, data.AuthTokens.Access, data.AuthTokens.Refresh
            ) );
        }

        [HttpPost, AllowAnonymous, ValidationErrorFilter]
        public IActionResult SignupUser([FromBody] SignupRequestDTO body) {
            if (!body.Validate()) return BadRequest(SignupResponseDTO.BadRequest());
            
            // Convert DTO --> raw signup data
            var raw = new SignupRaw {
                Email = body.email,
                Name = body.name,
                Password = body.password,
            };
            
            // SignupUser
            var data = _service.SignupUser(raw);

            if (!data.Success) {
                if (data.Error == SignupResponseError.EmailUniqueness) {
                    return BadRequest(SignupResponseDTO.EmailError());
                }

                return BadRequest(SignupResponseDTO.BadRequest());
            }
            
            
            return Ok(new SignupResponseDTO(true, data.UserId, data.Tokens));
        }

        [HttpPost("reauthenticate"), AllowAnonymous]
        public IActionResult Reauthenticate([FromBody] ReauthenticateRequestDto body) {
            // Validation
            var validationResult = body.Validate();
            if (validationResult != null) return BadRequest(validationResult);
            
            var data = _service.ReauthenticateUser(new ReauthenticateRequest {
                uid = body.uid,
                accessToken = body.tokens.access,
                refreshToken = body.tokens.refresh,
            });

            if (data.Success) return Ok(data);
            return new ObjectResult(data) { StatusCode = 401 };
        }

        [HttpGet("{id}"), Authorize]
        public IActionResult GetUser(long id) {
            long tokenId = int.Parse(User.Claims.First(x => x.Type == "uid").Value);
            if (id != tokenId) return new ObjectResult(new ForbiddenDto()) { StatusCode = 403 };

            var user = _service.GetUser(id);
            return Ok(new UserDetailDto(user));
        }

        [HttpPost("{id}/change-profile-image"), Authorize]
        public IActionResult ChangeProfileImage(long id, [FromForm] ChangeProfileImageDto body) {
            // Authorize user
            long tokenId = int.Parse(User.Claims.First(x => x.Type == "uid").Value);
            if (id != tokenId) return new ObjectResult(new ForbiddenDto()) { StatusCode = 403 };
            
            // Validate image size
            if (body.image.Length > 1e+6) {
                return BadRequest(new InvalidProfileImageSizeDto());
            }
            
            // Convert IFormFile --> byte[]
            var memoryStream = new MemoryStream();
            body.image.CopyTo(memoryStream);
            var fileBytes = memoryStream.ToArray();
            
            // Save image
            var path = _service.SaveUserProfileImage(id, fileBytes);
            _service.ChangeUserAvatarPath(id, path);
            
            return Ok(path);
        }
    }
}
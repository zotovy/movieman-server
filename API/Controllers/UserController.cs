using System;
using System.Collections.Generic;
using API.DTO.User;
using API.Filters;
using Domain;
using Domain.ValueObjects.User;
using Metadata.Services.UserMetadata;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.User;

namespace API.Controllers {
    [Route("api/{v:apiVersion}/users")]
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
        [AllowAnonymous, ValidationErrorFilterAttribute]
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
    }
}
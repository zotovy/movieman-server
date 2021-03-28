using System.Linq;
using API.DTO;
using API.DTO.Comment;
using API.DTO.Review;
using API.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Movie;
using Services.Review;
using Services.User;

namespace API.Controllers {
    [ApiController]
    [Route("api/{v:apiVersion}/review")]
    public class ReviewController : ControllerBase {
        
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;

        public ReviewController(IReviewService reviewService, IUserService userService) {
            _reviewService = reviewService;
            _userService = userService;
        }

        [HttpGet("{id}/comments"), AllowAnonymous]
        public IActionResult GetReviewComments(long id) {
            var comments = _reviewService.GetReviewComments(id);
            return Ok(comments.Select(x => new CommentDto(x)).ToList());
        }

        [HttpGet("{id}"), AllowAnonymous]
        public IActionResult GetReview(long id) {
            var review = _reviewService.GetReview(id);
            return Ok(new DetailReviewDto(review));
        }

        [HttpPost("{id}/comment"), Authorize, ValidationErrorFilter]
        public IActionResult CreateComment(long id, [FromBody] CreateCommentRequestDto body) {
            // Validate
            var validation = body.Validate();
            if (validation != null) return BadRequest(validation);
            
            // Authorize user
            long authorId = body.author;
            long tokenId = int.Parse(User.Claims.First(x => x.Type == "uid").Value);
            if (authorId != tokenId) return new ObjectResult(new ForbiddenDto()) { StatusCode = 403 };
            
            // Check is user with this id exists in db
            if (!_userService.IsUserExists(authorId)) {
                return BadRequest(new ValidateErrorDto(new ValidateErrorElement(
                    "author", "NotFound", "user with this id not found"
                )));
            }

            // check is review with this id exists in db
            if (!_reviewService.Exists(body.review)) {
                return BadRequest(new ValidateErrorDto(new ValidateErrorElement(
                    "review", "NotFound", "review with this id not found"
                )));
            }
            
            _reviewService.WriteComment(id, body.ToDomain());

            return Ok(new EmptyOkDto());
        }
    }
}
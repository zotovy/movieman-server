using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.DTO.Movie;
using API.DTO.Review;
using API.Filters;
using Domain.ValueObjects.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Movie;
using Services.User;

namespace API.Controllers {
    [ApiController]
    [Route("api/{v:apiVersion}/movie")]
    public class MovieController : ControllerBase {
        private readonly IMovieServices _movieServices;
        private readonly IUserService _userService;

        public MovieController(IMovieServices movieServices, IUserService userService) {
            _movieServices = movieServices;
            _userService = userService;
        }

        [HttpGet("/api/{v:apiVersion}/movies/popular")]
        public IActionResult GetPopularMovies() {
            var movies = _movieServices.GetPopularMovies();
            var dtos = movies.Select(x => new MovieDto(x)).ToList();
            return Ok(new MovieListDto(dtos));
        }

        [HttpPost]
        public async Task FetchPopularMovies() {
            await _movieServices.UpdatePopularMovies();
        }

        [HttpGet("search/{query}")]
        public async Task<IActionResult> SearchMovie(string query) {
            var movies = await _movieServices.SearchMovie(query);
            return Ok(new MovieListDto(
                movies.Select(x => new MovieDto(x)).ToList()
            ));
        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(long id) {
            var movie = _movieServices.GetMovie(id);
            if (movie == null) return NotFound(new NotFoundDto());
            return Ok(new MovieDetailDto(movie));
        }

        [HttpGet("/api/{v:apiVersion}/movies/get-by-genre/{genre}")]
        [AllowAnonymous, ValidationErrorFilter]
        public async Task<IActionResult> GetMovieByGenre(string genre) {
            var movies = await _movieServices.GetMoviesByGenre(new MovieGenre(genre));
            return Ok(new MovieListDto(
                movies.Select(x => new MovieDto(x)).ToList()
            ));
        }

        [HttpGet("{id}/reviews")]
        [AllowAnonymous, ValidationErrorFilter]
        public IActionResult GetMoviesReviews(long id) {
            var reviews = _movieServices.GetMoviesReviews(id);
            return Ok(reviews.Select(x => new DetailReviewDto(x)));
        }

        [HttpPost("{id}/review")]
        [Authorize, ValidationErrorFilter]
        public IActionResult WriteReview([FromBody] WriteReviewRequestDto body) {
            var errors = body.Validate();
            if (errors != null) return BadRequest(errors);

            // Authorize user
            long authorId = int.Parse(User.Claims.First(x => x.Type == "uid").Value);
            if (body.author != authorId) return new ObjectResult(new ForbiddenDto()) { StatusCode = 403 };

            // Check is user with this id exists in db
            if (!_userService.IsUserExists(authorId)) {
                return BadRequest(new ValidateErrorDto(new ValidateErrorElement(
                    "author", "NotFound", "user with this id not found"
                )));
            }

            // check is movie with this id exists in db
            if (!_movieServices.Exists(body.movie)) {
                return BadRequest(new ValidateErrorDto(new ValidateErrorElement(
                    "movie", "NotFound", "movie with this id not found"
                )));
            }
            
            // Write review
            _movieServices.WriteReview(body.movie, body.ToDomain());

            return Ok(new EmptyOkDto());
        }
    }
}
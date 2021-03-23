using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.DTO.Movie;
using API.Filters;
using Domain.ValueObjects.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Movie;

namespace API.Controllers {
    [ApiController]
    [Route("api/{v:apiVersion}/movie")]
    public class MovieController: ControllerBase {

        private readonly IMovieServices _movieServices;

        public MovieController(IMovieServices movieServices) {
            _movieServices = movieServices;
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
            return Ok(new MovieDto(movie));
        }

        [HttpGet("/api/{v:apiVersion}/movies/get-by-genre/{genre}")]
        [AllowAnonymous, ValidationErrorFilter]
        public async Task<IActionResult> GetMovieByGenre(string genre) {
            var movies = await _movieServices.GetMoviesByGenre(new MovieGenre(genre));
            return Ok(new MovieListDto(
                movies.Select(x => new MovieDto(x)).ToList()
            ));
        }
    }
}
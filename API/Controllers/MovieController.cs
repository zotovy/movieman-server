using System.Linq;
using System.Threading.Tasks;
using API.DTO.Movie;
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
    }
}
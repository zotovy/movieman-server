using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.ExternalMovieApi;
using Services.Movie;

namespace API.Controllers {
    [ApiController]
    [Route("api/{v:apiVersion}/movie")]
    public class MovieController: ControllerBase {

        private readonly IMovieServices _movieServices;

        public MovieController(IMovieServices movieServices) {
            _movieServices = movieServices;
        }

        [HttpGet]
        public async Task<dynamic> GetPopularMovies() {
            // await _movieServices.GetPopularMovies();
            return true;
        }
        
    }
}
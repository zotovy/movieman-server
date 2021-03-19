using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.ExternalMovieApi;

namespace API.Controllers {
    [ApiController]
    [Route("api/{v:apiVersion}/movie")]
    public class MovieController: ControllerBase {

        private readonly IExternalMovieApiService _externalMovieApiService;

        public MovieController(IExternalMovieApiService kinopoiskService) {
            _externalMovieApiService = kinopoiskService;
        }
        
        [HttpGet]
        public async Task<dynamic> GetPopularMovies() {
            return await _externalMovieApiService.GetPopularMovies();
            return true;
        }
        
    }
}
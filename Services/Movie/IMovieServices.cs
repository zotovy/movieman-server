using System.Threading.Tasks;

namespace Services.Movie {
    public interface IMovieServices {
        public Task UpdatePopularMovies();
    }
}
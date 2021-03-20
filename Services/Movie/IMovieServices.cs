using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Services.Movie {
    public interface IMovieServices {
        public Task UpdatePopularMovies();
        public ImmutableList<Domain.Movie> GetPopularMovies();
    }
}
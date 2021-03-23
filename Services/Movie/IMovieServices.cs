using System.Collections.Immutable;
using System.Threading.Tasks;
using Domain.ValueObjects.Movie;

namespace Services.Movie {
    public interface IMovieServices {
        public Task UpdatePopularMovies();
        public ImmutableList<Domain.Movie> GetPopularMovies();
        public Task<ImmutableList<Domain.Movie>> SearchMovie(string name);
        public Domain.Movie GetMovie(long id);
        public void CreateMovie(Domain.Movie movie);
        public Task<ImmutableList<Domain.Movie>> GetMoviesByGenre(MovieGenre genre);
    }
}
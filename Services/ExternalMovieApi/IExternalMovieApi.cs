using System.Collections.Immutable;
using System.Threading.Tasks;
using Domain.ValueObjects.Movie;

namespace Services.ExternalMovieApi {
    public interface IExternalMovieApiServices {
        public Task<ImmutableList<Domain.Movie>> GetPopularMovies();
        public Task<ImmutableList<Domain.Movie>> SearchMovie(string name);
        public Task<ImmutableList<Domain.Movie>> GetMoviesByGenre(MovieGenre genre);
    }
}
using System.Collections.Immutable;
using System.Threading.Tasks;
using Domain;

namespace Services.ExternalMovieApi {
    public interface IExternalMovieApiService {
        public Task<ImmutableList<Movie>> GetPopularMovies();
    }
}
using System.Collections.Immutable;
using System.Threading.Tasks;
using Domain;

namespace Services.ExternalMovieApi {
    public interface IExternalMovieApiServices {
        public Task<ImmutableList<Domain.Movie>> GetPopularMovies();
    }
}
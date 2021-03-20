using System.Collections.Generic;
using System.Collections.Immutable;

namespace Database.Movie {
    public interface IMovieRepository {
        public void SaveChanges();
        public void UpdatePopularMovies(IEnumerable<Domain.Movie> movies);
        public ImmutableList<Domain.Movie> GetPopularMovies();
        #nullable enable
        public Domain.Movie? GetMovie(long id);
        #nullable enable
        public Domain.Movie? GetMovieByExternalId(long kpId);
        public void AddMovie(Domain.Movie movie);
    }
}
using System.Collections.Generic;

namespace Database.Movie {
    public interface IMovieRepository {
        public void SaveChanges();
        public void UpdatePopularMovies(IEnumerable<Domain.Movie> movies);
    }
}
using System.Collections.Immutable;
using System.Threading.Tasks;
using Domain.ValueObjects.Movie;

namespace Services.Movie {
    public interface IMovieServices {
        public Task UpdatePopularMovies();
        public ImmutableList<Domain.Movie> GetPopularMovies();
        public Task<ImmutableList<Domain.Movie>> SearchMovie(string name);
        public Domain.Movie GetMovie(long id);
        public Task<ImmutableList<Domain.Movie>> GetMoviesByGenre(MovieGenre genre);
        public void WriteReview(long movieId, Domain.Review review);
        public ImmutableList<Domain.Review> GetMoviesReviews(long id);
        public bool Exists(long id);
    }
}
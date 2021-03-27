using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Database.Movie;
using Database.Review;
using Domain;
using Domain.ValueObjects.Movie;
using Services.ExternalMovieApi;

namespace Services.Movie {
    public sealed class MovieServices : IMovieServices {
        private readonly IMovieRepository _movieRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IExternalMovieApiServices _externalMovieApiServices;

        public MovieServices(IMovieRepository movieRepository, IReviewRepository reviewRepository, IExternalMovieApiServices externalMovieApiServices) {
            _movieRepository = movieRepository;
            _reviewRepository = reviewRepository;
            _externalMovieApiServices = externalMovieApiServices;
        }

        public async Task UpdatePopularMovies() {
            var movies = await _externalMovieApiServices.GetPopularMovies();
            _movieRepository.UpdatePopularMovies(movies);
            _movieRepository.SaveChanges();
        }

        public ImmutableList<Domain.Movie> GetPopularMovies() => _movieRepository.GetPopularMovies();

        private ImmutableList<Domain.Movie> SaveNotSavedMovies(IEnumerable<Domain.Movie> movies) {
            // need this list to save new generated id of not saved in database movies 
            var newMovies = new List<Domain.Movie>();
            
            // go throw fetched movies and save not existing movies
            foreach (var movie in movies) {
                var model = _movieRepository.GetMovieByExternalId(movie.KpId);
                if (model == null) {
                    _movieRepository.AddMovie(movie);
                    _movieRepository.SaveChanges();
                    model = _movieRepository.GetMovieByExternalId(movie.KpId);
                    newMovies.Add(model);
                }
                else {
                    newMovies.Add(model);
                }
            }

            return newMovies.ToImmutableList();
        }

        public async Task<ImmutableList<Domain.Movie>> SearchMovie(string name) {
            // get movies from external api
            var movies = await _externalMovieApiServices.SearchMovie(name);
            return SaveNotSavedMovies(movies);
        }
        

        public Domain.Movie GetMovie(long id) => _movieRepository.GetMovie(id);

        public async Task<ImmutableList<Domain.Movie>> GetMoviesByGenre(MovieGenre genre) {
            // get movies from external api
            var movies = await _externalMovieApiServices.GetMoviesByGenre(genre);
            return SaveNotSavedMovies(movies);
        }

        public void WriteReview(long movieId, Review review) {
            // переписать этот пиздец 
            var model = _reviewRepository.CreateReview(review);
            _movieRepository.SaveChanges(); // used to update review id
            var movie = _movieRepository.AddReview(movieId, model.Id);
            _movieRepository.AddNewRating(movie, review.Rating);
            _movieRepository.SaveChanges();
        }

        public ImmutableList<Review> GetMoviesReviews(long id) => _reviewRepository.GetMoviesReviews(id);

        public bool Exists(long id) => GetMovie(id) != null;
    }
}
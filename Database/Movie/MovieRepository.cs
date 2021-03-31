using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Database.Review;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Database.Movie {
    public class MovieRepository : IMovieRepository {
        private readonly DatabaseContext _context;

        public MovieRepository(DatabaseContext context) {
            _context = context;
        }

        public bool IsMovieExists(long id) {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            return movie != null;
        }

        public void SaveChanges() => _context.SaveChanges();

        public void UpdatePopularMovies(IEnumerable<Domain.Movie> movies) {

            // Remove old popular movies
            _context.LinkToPopularMovieModels.RemoveRange(_context.LinkToPopularMovieModels.ToList());

            // Go throw movies and checked is this movie exists in db
            foreach (var movie in movies) {
                var founded = _context.Movies.FirstOrDefault(m => m.KpId == movie.KpId);

                // Create a MovieModel to work with db later
                var model = new MovieModel(movie);

                if (founded == null) {
                    // Add this movie to db context
                    _context.Movies.Add(model);
                }

                SaveChanges();

                // Extract Id and save this Id as a link to LinkToPopularMovieModel collection
                _context.LinkToPopularMovieModels.Add(new LinkToPopularMovieModel(model.KpId));
            }
        }

        public ImmutableList<Domain.Movie> GetPopularMovies() {
            // get ids of popular movies
            var ids = _context.LinkToPopularMovieModels.Select(x => x.Id).ToList();

            // grab this movies by ids from database
            List<Domain.Movie> movies = new List<Domain.Movie>();
            foreach (var id in ids) {
                var model = _context.Movies.First(m => m.KpId == id);
                movies.Add(model.ToDomain());
            }

            return movies.ToImmutableList();
        }

        public void AddMovie(Domain.Movie movie) {
            var model = new MovieModel(movie);
            _context.Movies.Add(model);
        }

        #nullable enable
        public Domain.Movie? GetMovie(long id) {
            var model = _context.Movies.FirstOrDefault(x => x.Id == id);
            return model == null ? null : model.ToDomain();
        }

        #nullable enable
        public Domain.Movie? GetMovieByExternalId(long kpId) {
            var model = _context.Movies.FirstOrDefault(x => x.KpId == kpId);
            return model == null ? null : model.ToDomain();
        }

        public Domain.Movie AddReview(long movieId, Domain.Review review) {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == movieId);

            // Throw error if no movie found
            if (movie == null || movie.Reviews == null) throw new ArgumentException("No movie found");
            
            var amountOfReviews = movie.ReviewIds.Select(x => x).ToList().Count;
            if (amountOfReviews == 0) amountOfReviews = 1;
            
            var average = movie.Rating * (amountOfReviews - 1);
            var newAverage = average + review.Rating.Value;
            var newRating = newAverage / amountOfReviews;
            
            movie.ReviewIds.Add(review.Id);
            movie.Rating = newRating;
            SaveChanges();

            movie.Reviews = null;
            
            return movie.ToDomain();
        }

        public void AddNewRating(long movieId, Rating rating) {
            // var movie = _context.Movies.FirstOrDefault(x => x.Id == movieId);
            // if (movie == null) return;
            //
            //
            //
            // var model = _context.Movies.FirstOrDefault(x => x.Id == movie.Id);
            // if (model == null) return;
            //
            // model.Rating = newRating;
            // SaveChanges();
        }
    }
}
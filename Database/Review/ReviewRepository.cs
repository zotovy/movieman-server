using System.Collections.Immutable;
using System.Linq;
using Domain;

namespace Database.Review {
    public sealed class ReviewRepository: IReviewRepository {
        private readonly DatabaseContext _context;

        public ReviewRepository(DatabaseContext context) {
            _context = context;
        }

        public ImmutableList<Domain.Review> GetMoviesReviews(long movieId) {
            var reviews = _context.Reviews
                .Where(x => x.Movie == movieId)
                .Select(x => x.ToDomain())
                .ToList();
            
            // populate reviews
            foreach (var review in reviews) {
                var movie = _context.Movies.First(x => x.Id == review.Movie.Id);
                var author = _context.Users.First(x => x.Id == review.Author.Id);
                var comments = _context.Comments
                    .Where(x => x.Review == review.Id)
                    .ToList();

                review.Movie.Model = movie.ToDomain();
                review.Author.Model = author.ToDomain();
                review.Comments = comments
                    .Select(x => new Ref<Domain.Comment>(x.Id, x.ToDomain()))
                    .ToList();
            }

            return reviews.ToImmutableList();
        }

        public ReviewModel CreateReview(Domain.Review review) {
            var model = new ReviewModel(review);
            _context.Reviews.Add(model);
            return model;
        }
    }
}
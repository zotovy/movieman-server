using System.Collections.Immutable;
using System.Linq;
using Database.Comment;
using Domain;

namespace Database.Review {
    public sealed class ReviewRepository: IReviewRepository {
        private readonly DatabaseContext _context;

        public ReviewRepository(DatabaseContext context) {
            _context = context;
        }

        public bool Exists(long id) {
            return _context.Reviews.FirstOrDefault(x => x.Id == id) != null;
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

                for (int i = 0; i < comments.Count; i++) {
                    var commentAuthor = _context.Users.First(x => x.Id == comments[i].Author);
                    review.Comments[0].Model.Author = new Ref<Domain.User>(commentAuthor.Id, commentAuthor.ToDomain());
                }
            }

            return reviews.ToImmutableList();
        }

        public ReviewModel AddReview(Domain.Review review) {
            var model = new ReviewModel(review);
            _context.Reviews.Add(model);
            return model;
        }

        public void AddCommentToReview(long id, Domain.Comment comment) {
            var model = _context.Reviews.First(x => x.Id == id);
            model.Comments.Add(comment.Id);
        }
    }
}
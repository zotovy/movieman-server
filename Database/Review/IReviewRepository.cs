using System.Collections.Immutable;

namespace Database.Review {
    public interface IReviewRepository {
        public bool Exists(long id);
        public ImmutableList<Domain.Review> GetMoviesReviews(long movieId);
        public ReviewModel AddReview(Domain.Review review);
        public void AddCommentToReview(long id, long commentId);
        public Domain.Review? GetReview(long id);
    }
}
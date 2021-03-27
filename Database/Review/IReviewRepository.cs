using System.Collections.Immutable;

namespace Database.Review {
    public interface IReviewRepository {
        public ImmutableList<Domain.Review> GetMoviesReviews(long movieId);
        public ReviewModel CreateReview(Domain.Review review);
    }
}
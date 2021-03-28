using System.Collections.Immutable;

namespace Services.Review {
    public interface IReviewService {
        public bool Exists(long id);
        public void WriteComment(long id, Domain.Comment comment);
        public ImmutableList<Domain.Comment> GetReviewComments(long id);
        public Domain.Review GetReview(long id);
    }
}
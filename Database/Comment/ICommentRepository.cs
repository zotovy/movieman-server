using System.Collections.Immutable;

namespace Database.Comment {
    public interface ICommentRepository {
        public void SaveChanges();
        public CommentModel AddModel(Domain.Comment comment);
        public ImmutableList<Domain.Comment> GetReviewComments(long id);
    }
}
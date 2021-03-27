namespace Database.Comment {
    public interface ICommentRepository {
        public void SaveChanges();
        public CommentModel AddModel(Domain.Comment comment);
    }
}
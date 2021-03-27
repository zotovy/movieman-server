namespace Database.Comment {
    public class CommentRepository : ICommentRepository {
        private readonly DatabaseContext _context;

        public CommentRepository(DatabaseContext context) {
            _context = context;
        }

        public void SaveChanges() => _context.SaveChanges();

        public CommentModel AddModel(Domain.Comment comment) {
            var model = new CommentModel(comment);
            _context.Comments.Add(model);
            return model;
        }
    }
}
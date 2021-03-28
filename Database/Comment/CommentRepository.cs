using System.Collections.Immutable;
using System.Data.Entity;
using System.Linq;
using Database.Movie;

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

        public ImmutableList<Domain.Comment> GetReviewComments(long id) {
            var comments = (
                from comment in _context.Comments
                where comment.ReviewId == id
                join author in _context.Users on comment.Author equals author
                select new CommentModel {
                    Author = author,
                    Content = comment.Content,
                    Id = comment.Id,
                    Review = comment.Review,
                    ReviewId = comment.ReviewId,
                    AuthorId = comment.AuthorId,
                    CreatedAt = comment.CreatedAt,
                }).ToList();
            
            return comments.Select(x => x.ToDomain()).ToImmutableList();
        }
    }
}
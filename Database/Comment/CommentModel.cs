using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Review;
using Database.User;
using Domain;
using Domain.ValueObjects.Comment;

namespace Database.Comment {
    public class CommentModel {
        
        [Key]
        public long Id { get; set; }
        public long AuthorId { get; set; }
        public UserModel? Author { get; set; }
        public long ReviewId { get; set; }
        public ReviewModel? Review { get; set; }
        [Column("Content", TypeName = "varchar(1000)")]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public CommentModel() { }

        public CommentModel(Domain.Comment comment) {
            Id = comment.Id;
            AuthorId = comment.Author.Id;
            Content = comment.Content.Value;
            ReviewId = comment.Review.Id;
            CreatedAt = comment.CreatedAt;
            Author = comment.Author.Model != null ? new UserModel(comment.Author.Model) : null;
            Review = comment.Review.Model != null ? new ReviewModel(comment.Review.Model) : null;
        }

        public Domain.Comment ToDomain() {
            return new Domain.Comment {
                Author = new Ref<Domain.User>(AuthorId, Author?.ToDomain()),
                Content = new CommentContent(Content),
                Id = Id,
                CreatedAt = CreatedAt,
                Review = new Ref<Domain.Review>(ReviewId, Review?.ToDomain())
            };
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;
using Domain.ValueObjects.Comment;

namespace Database.Comment {
    public sealed record CommentModel {
        
        [Key]
        public long Id { get; set; }
        [ForeignKey("User")]
        public long Author { get; set; }
        [Column("Content", TypeName = "varchar(1000)")]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Domain.Comment ToDomain() {
            return new Domain.Comment {
                Author = new Ref<Domain.User>(Author),
                Content = new CommentContent(Content),
                Id = Id,
                CreatedAt = CreatedAt,
            };
        }
    }
}
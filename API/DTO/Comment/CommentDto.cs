using System;
using API.DTO.User;

namespace API.DTO.Comment {
    public sealed class CommentDto {
        public long id { get; set; }
        public long review { get; set; }
        public UserTileDTO author { get; set; }
        public string content { get; set; }
        public DateTime createdAt { get; set; }

        public CommentDto(Domain.Comment comment) {
            id = comment.Id;
            review = comment.Review.Id;
            author = new UserTileDTO(comment.Author.Model);
            content = comment.Content.Value;
            createdAt = comment.CreatedAt;
        }
    }
}

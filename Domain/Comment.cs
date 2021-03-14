using System;
using Domain.ValueObjects.Comment;

namespace Domain {
    public sealed class Comment {
        public long Id { get; set; }
        public Ref<User> Author { get; set; }
        public CommentContent Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
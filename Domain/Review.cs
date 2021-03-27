using System;
using System.Collections.Generic;
using Domain.ValueObjects;
using Domain.ValueObjects.Review;

namespace Domain {
    public sealed class Review {
        public long Id { get; set; }
        public Ref<Movie> Movie { get; set; } 
        public Ref<User> Author { get; set; } 
        public List<Ref<Comment>> Comments { get; set; }
        public ReviewContent Content { get; set; }
        public Rating Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
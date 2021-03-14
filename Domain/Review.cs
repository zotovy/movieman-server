using System;
using System.Collections.Generic;
using Domain.ValueObjects;
using Domain.ValueObjects.Review;

namespace Domain {
    public class Review {
        public long Id { get; set; }
        public Ref<Movie> Movie { get; set; } 
        public Ref<User> Author { get; set; } 
        public IEnumerable<Ref<dynamic>> Comments { get; set; } // todo
        public ReviewContent Content { get; set; }
        public Rating Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
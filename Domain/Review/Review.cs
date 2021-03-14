using System;
using System.Collections.Generic;
using Domain.Review.ValueObjects;
using Domain.ValueObjects;

namespace Domain.Review {
    public class Review {
        public long Id { get; set; }
        public Ref<dynamic> Movie { get; set; } // todo
        public Ref<dynamic> Author { get; set; } // todo
        public IEnumerable<Ref<dynamic>> Comments { get; set; } // todo
        public ReviewContent Content { get; set; }
        public Rating Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
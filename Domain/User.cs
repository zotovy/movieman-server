using System;
using System.Collections.Generic;
using Domain.ValueObjects.Movie;
using Domain.ValueObjects.User;

namespace Domain {
    public sealed class User {
        public long Id { get; set; }
        public Name Name { get; set; }
        public List<Ref<Movie>> Movies { get; set; }
        public List<Ref<Review>> Reviews { get; set; }
        public List<Ref<Comment>> Comments { get; set; } 
        public Email Email { get; set; }
        public Password Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public ImagePath ProfileImagePath { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Domain.ValueObjects.User;

namespace Domain {
    public class User {
        public long Id { get; set; }
        public Name Name { get; set; }
        public IEnumerable<Ref<Movie>> Movies { get; set; }
        public IEnumerable<Ref<Review>> Reviews { get; set; }
        public IEnumerable<Ref<dynamic>> Comments { get; set; } // todo
        public Email Email { get; set; }
        public Password Password { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
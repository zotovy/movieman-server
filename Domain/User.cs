using Domain.ValueObjects;

namespace Domain {
    public class User {
        public long Id { get; set; }
        public Name Name { get; set; }
        public Email Email { get; set; }
        public Password Password { get; set; }
        public Age Age { get; set; }
    }
}
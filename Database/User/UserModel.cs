using Domain.ValueObjects.User;

namespace Database.User {
    public sealed record UserModel {
        
        public long Id { get; init; }
        
        public string Name { get; init; }
        
        public string Email { get; init; }
        
        public string Password { get; init; }

        public static  Domain.User ToDomain(UserModel model) {
            return new Domain.User {
                Id = model.Id,
                Email = new Email(model.Email),
                Password = new Password(model.Password),
                Name = new Name(model.Name),
            };
        } 
        
        // todo: ... 
    }
}
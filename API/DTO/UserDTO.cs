namespace API.DTO {
    public sealed record UserDTO {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }

        public static UserDTO FromDomain(Domain.User user) {
            return new UserDTO {
                Id = user.Id,
                Name = user.Name.Value,
                Email = user.Email.Value,
                Password = user.Password.Value,
            };
        } 
        
    }
}
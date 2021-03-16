using System.Collections.Generic;
using Domain.ValueObjects.User;
using Metadata.Services.UserMetadata;

namespace Services.User {
    public interface IUserService {
        public IEnumerable<Domain.User> GetUsers();
        public LoginResponse LoginUser(Email email, Password password);
    }
}
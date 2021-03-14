using System.Collections.Generic;

namespace Services.User {
    public interface IUserService {
        public IEnumerable<Domain.User> GetUsers();
    }
}
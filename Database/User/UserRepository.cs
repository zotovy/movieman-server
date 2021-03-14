using System.Collections.Generic;
using System.Linq;

namespace Database.User {
    public class UserRepository: IUserRepository {

        private readonly UserContext _context;

        public UserRepository(UserContext context) {
            _context = context;
        }

        public IReadOnlyList<UserModel> GetModels() {
            return _context.Users.ToList();
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace Database.User {
    public class UserRepository: IUserRepository {

        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context) {
            _context = context;
        }

        public IReadOnlyList<UserModel> GetModels() {
            return _context.Users.ToList();
        }
    }
}
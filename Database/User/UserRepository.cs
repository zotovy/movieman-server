using System.Collections.Generic;
using System.Linq;
using Domain.ValueObjects.User;

namespace Database.User {
    public class UserRepository: IUserRepository {

        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context) {
            _context = context;
        }

        public IReadOnlyList<UserModel> GetModels() {
            return _context.Users.ToList();
        }

        public UserModel FoundWithSameEmailAndPassword(Email email, Password password) {
            var _email = email.Value;
            var _password = password.Value;

            return _context.Users.FirstOrDefault(
                u => u.Email == _email && u.Password == _password
            );
        } 

        public bool ExistsWithSameEmailAndPassword(Email email, Password password) {
            return FoundWithSameEmailAndPassword(email, password) != null;
        }

        public bool ExistsWithSameEmailAndPassword(Domain.User user) {
            return FoundWithSameEmailAndPassword(user.Email, user.Password) != null;
        }
    }
}
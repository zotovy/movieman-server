using System.Collections.Generic;
using System.Linq;
using Database.User;

namespace Services.User {
    public class UserServices: IUserService {
        
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public IEnumerable<Domain.User> GetUsers() {
            var models =  _userRepository.GetModels();
            return models.Select(UserModel.ToDomain);
        }
    }
}
using System.Collections.Generic;

namespace Database.User {
    public interface IUserRepository {
        public IReadOnlyList<UserModel> GetModels();
    }
}
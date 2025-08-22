using GhostText.Models.Users;
using GhostText.Repositories.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository) =>
            this.userRepository = userRepository;

        public async ValueTask<User> AddUserAsync(User user) 
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            user.CreatedAt = now;
            user.UpdatedAt = now;

            return await this.userRepository.InsertUserAsync(user);
        }

        public IQueryable<User> RetrieveAllUsers() =>
            this.userRepository.SelectAllUsers();
    }
}

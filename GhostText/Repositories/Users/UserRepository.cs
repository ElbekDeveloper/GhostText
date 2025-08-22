using System;
using System.Linq;
using System.Threading.Tasks;
using GhostText.Data;
using GhostText.Models.Users;

namespace GhostText.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        public UserRepository(ApplicationDbContext dbContext) =>
            this.dbContext = dbContext;

        public async ValueTask<User> InsertUserAsync(User user)
        {
            await this.dbContext.Users.AddAsync(user);
            await this.dbContext.SaveChangesAsync();

            return user;
        }

        public IQueryable<User> SelectAllUsers() =>
            this.dbContext.Users;
    }
}

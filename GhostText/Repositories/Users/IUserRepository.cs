using GhostText.Models.Users;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Repositories.Users
{
    public interface IUserRepository
    {
        ValueTask<User> InsertUserAsync(User user);
        IQueryable<User> SelectAllUsers();
    }
}

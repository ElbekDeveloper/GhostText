using GhostText.Models.Users;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Services.Users
{
    public interface IUserService
    {
        ValueTask<User> AddUserAsync(User user);
        IQueryable<User> RetrieveAllUsers();
    }
}

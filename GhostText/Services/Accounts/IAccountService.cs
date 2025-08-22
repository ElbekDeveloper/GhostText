using GhostText.Models.UserCredentials;
using GhostText.Models.UserTokens;
using System.Threading.Tasks;

namespace GhostText.Services.Accounts
{
    public interface IAccountService
    {
        ValueTask<UserToken> LoginAsync(UserCredential userCredential);
    }
}

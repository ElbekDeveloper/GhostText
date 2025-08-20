using System;
using System.Linq;
using System.Threading.Tasks;
using GhostText.Models.Connects;

namespace GhostText.Services.Connects;

public interface IConnectService
{
    ValueTask<ConnectToken>AddTokenAsync(ConnectToken connectToken);
    IQueryable<ConnectToken> RetrieveAllTokens();
    ValueTask<ConnectToken> RetrieveTokenByIdAsync(Guid id);
    ValueTask<ConnectToken> ModifyTokenAsync(ConnectToken connectToken);
    ValueTask<ConnectToken> RemoveTokenByIdAsync(Guid id);

    ValueTask<ConnectToken> BringChanelIdAsync(string botTokenId);
}
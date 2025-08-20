using System;
using System.Linq;
using System.Threading.Tasks;
using GhostText.Models.Connects;

namespace GhostText.Repositories.Connects;

public interface IConnectRepository
{
    ValueTask<ConnectToken> InsertTokenAsync(ConnectToken connectToken);
    IQueryable<ConnectToken> SelectAllTokens();
    ValueTask<ConnectToken> SelectTokenByIdAsync(Guid tokenId);
    ValueTask<ConnectToken> UpdateTokenAsync(ConnectToken connectToken);
    ValueTask<ConnectToken> DeleteTokenAsync(ConnectToken connectToken);
}
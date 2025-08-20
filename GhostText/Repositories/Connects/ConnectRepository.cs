using System;
using System.Linq;
using System.Threading.Tasks;
using GhostText.Data;
using GhostText.Models.Connects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GhostText.Repositories.Connects;

public class ConnectRepository: IConnectRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public ConnectRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async ValueTask<ConnectToken> InsertTokenAsync(ConnectToken connectToken)
    {
        await applicationDbContext.ConnectTokens.AddAsync(connectToken);
        await applicationDbContext.SaveChangesAsync();
        return connectToken;
    }

    public IQueryable<ConnectToken> SelectAllTokens()
    {
        return applicationDbContext.ConnectTokens;
    }

    public async ValueTask<ConnectToken> SelectTokenByIdAsync(Guid tokenId)
    {
        return await applicationDbContext.ConnectTokens.FindAsync(tokenId);
    }

    public async ValueTask<ConnectToken> UpdateTokenAsync(ConnectToken connectToken)
    {
        this.applicationDbContext.ConnectTokens.Entry(connectToken).State=EntityState.Modified;
        await applicationDbContext.SaveChangesAsync();
        return connectToken;
    }

    public async ValueTask<ConnectToken> DeleteTokenAsync(ConnectToken connectToken)
    {
        this.applicationDbContext.ConnectTokens.Entry(connectToken).State = EntityState.Deleted;
        await applicationDbContext.SaveChangesAsync();
        return connectToken;
    }
}
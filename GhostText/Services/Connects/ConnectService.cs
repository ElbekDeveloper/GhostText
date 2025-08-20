using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhostText.Models.Connects;
using GhostText.Repositories.Connects;
using Microsoft.EntityFrameworkCore;

namespace GhostText.Services.Connects;

public class ConnectService: IConnectService
{
    private readonly IConnectRepository connectRepository;

    public ConnectService(IConnectRepository connectRepository)
    {
        this.connectRepository = connectRepository;
    }

    public async ValueTask<ConnectToken> AddTokenAsync(ConnectToken connectToken)
    {
        return await this.connectRepository.InsertTokenAsync(connectToken);
    }

    public IQueryable<ConnectToken> RetrieveAllTokens()
    {
        return this.connectRepository.SelectAllTokens();
    }

    public async ValueTask<ConnectToken> RetrieveTokenByIdAsync(Guid id)
    {
        ConnectToken connectToken=await this.connectRepository.SelectTokenByIdAsync(id);

        if (connectToken is null)
        {
            throw new KeyNotFoundException("Token not found");
        }
        
        return connectToken;
    }

    public async ValueTask<ConnectToken> ModifyTokenAsync(ConnectToken connectToken)
    {
        ConnectToken newConnectToken=
            await this.connectRepository.SelectTokenByIdAsync(connectToken.Id);
        
        if (newConnectToken is null)
        {
            throw new KeyNotFoundException("Token not found");
        }
        
        return connectToken;
    }

    public async ValueTask<ConnectToken> RemoveTokenByIdAsync(Guid id)
    {
       ConnectToken connectToken=
           await this.connectRepository.SelectTokenByIdAsync(id);
       
       if (connectToken is null)
       {
            throw new KeyNotFoundException("Token not found");
       }
       
       return connectToken;
    }

    public async ValueTask<ConnectToken> BringChanelIdAsync(string botTokenId)
    {
        ConnectToken connectToken=
            await this.RetrieveAllTokens().FirstOrDefaultAsync(connectToken=> connectToken.BotTokenId == botTokenId);
        
        if (connectToken is null)
        {
            throw new KeyNotFoundException("Token not found");
        }
        
        return connectToken;
    }
}
using System;
using System.Threading.Tasks;
using GhostText.Models;

namespace GhostText.Repositories
{
    public interface IMessageRepository
    {
        Task<Message> InsertMessageAsync(Message message);
        
        Task<Message> DeleteMessageAsync(Message message);
    }
}

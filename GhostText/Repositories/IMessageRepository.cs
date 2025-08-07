using GhostText.Models;
using System.Threading.Tasks;

namespace GhostText.Repositories
{
    public interface IMessageRepository
    {
        Task<Message> InsertMessageAsync(Message message);
        Task<Message> UpdateMessageAsync(Message message);
    }
}
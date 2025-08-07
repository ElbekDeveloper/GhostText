using GhostText.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Repositories
{
    public interface IMessageRepository
    {
        Task<Message> InsertMessageAsync(Message message);
        IQueryable<Message> SelectAllMessages();
        Task<Message> UpdateMessageAsync(Message message);
    }
}
using GhostText.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Services
{
    public interface IMessageService
    {
        Task<Message> AddMessageAsync(Message message);
        IQueryable<Message> RetrieveAllMessages();
    }
}
using GhostText.Models;

namespace GhostText.Services
{
    public interface IMessageService
    {
        Task<Message> AddMessageAsync(Message message);
        IQueryable<Message> RetrieveAllMessage();

    }
}

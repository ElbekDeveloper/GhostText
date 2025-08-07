using GhostText.Models;

namespace GhostText.Repositories
{
    public interface IMessageRepository
    {
        Task<Message> InsertMessageAsync(Message message);
        Task<Message> SelectMessageByIdAsync(Guid massageId);
    }
}

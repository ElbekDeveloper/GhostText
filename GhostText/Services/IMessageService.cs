using GhostText.Models;

namespace GhostText.Services
{
    public interface IMessageService
    {
        Task<Message> AddMessageAsync(Message message);
        
        IQueryable<Message> RetrieveAllMessage();
        
        Task<Message> RetrieveMessageByYdAsync(Guid messageId);
        
        Task<Message> ModifyMessageAsync(Message message);
        
        Task<Message> DeleteMessageAsync(Guid messageId);

    }
}

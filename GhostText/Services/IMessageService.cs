using System;
using GhostText.Models;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace GhostText.Services
{
    public interface IMessageService
    {
        Task<Message> AddMessageAsync(Message message);
        IQueryable<Message> RetrieveAllMessages();
        Task<Message> RetrieveMessageByIdAsync(Guid messageId);
        Task<Message> ModifyMessageAsync(Message message);
        Task<Message> DeleteMessageAsync(Guid id);
    }
}
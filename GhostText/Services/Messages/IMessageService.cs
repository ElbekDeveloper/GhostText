using System;
using GhostText.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Services
{
    public interface IMessageService
    {
        ValueTask<Message> AddMessageAsync(Message message);
        IQueryable<Message> RetrieveAllMessages();
        ValueTask<Message> RetrieveMessageByIdAsync(Guid messageId);
        ValueTask<Message> ModifyMessageAsync(Message message);
        ValueTask<Message> RemoveMessageByIdAsync(Guid Id);
    }
}
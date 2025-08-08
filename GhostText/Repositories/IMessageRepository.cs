using System;
using System.Threading.Tasks;
using GhostText.Models;
using System.Linq;

namespace GhostText.Repositories
{
    public interface IMessageRepository
    {
        Task<Message> InsertMessageAsync(Message message);
        IQueryable<Message> SelectAllMessages();
        Task<Message> SelectMessageByIdAsync(Guid messageId);
        Task<Message> UpdateMessageAsync(Message message);
        Task<Message> DeleteMessageAsync(Message message);
    }
}
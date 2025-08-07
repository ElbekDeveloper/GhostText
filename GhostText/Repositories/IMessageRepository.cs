using GhostText.Models;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace GhostText.Repositories
{
    public interface IMessageRepository
    {
        Task<Message> InsertMessageAsync(Message message);
        IQueryable<Message> SelectAllMessages();
        Task<Message> SelectMessageByIdAsync(Guid massageId);
        Task<Message> UpdateMessageAsync(Message message);
    }
}
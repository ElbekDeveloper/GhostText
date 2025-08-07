using GhostText.Models;
using System;
using System.Threading.Tasks;

namespace GhostText.Repositories
{
    public interface IMessageRepository
    {
        Task<Message> InsertMessageAsync(Message message);
        Task<Message> SelectMessageByIdAsync(Guid massageId);
        Task<Message> UpdateMessageAsync(Message message);
    }
}
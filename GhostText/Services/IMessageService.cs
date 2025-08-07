using System;
using GhostText.Models;
using System.Threading.Tasks;

namespace GhostText.Services
{
    public interface IMessageService
    {
        Task<Message> AddMessageAsync(Message message);
        Task<Message> RetrieveMessageByIdAsync(Guid messageId);
    }
}
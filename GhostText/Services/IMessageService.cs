using GhostText.Models;
using System;
using System.Threading.Tasks;

namespace GhostText.Services
{
    public interface IMessageService
    {
        Task<Message> AddMessageAsync(Message message);

        Task<Message> ModifyMessageAsync(Message message);

        Task<Message> DeleteMessageAsync(Guid id);
    }
}
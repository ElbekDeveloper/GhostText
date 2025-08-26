using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GhostText.Models;
using System.Linq;

namespace GhostText.Repositories
{
    public interface IMessageRepository
    {
        ValueTask<Message> InsertMessageAsync(Message message);
        IQueryable<Message> SelectAllMessages();
        ValueTask<Message> SelectMessageByIdAsync(Guid messageId);
        ValueTask<Message> UpdateMessageAsync(Message message);
        ValueTask<Message> DeleteMessageAsync(Message message);
        ValueTask RemoveRangeAsync(List<Message> messages);
    }
}
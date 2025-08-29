using GhostText.Data;
using GhostText.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostText.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        
        public MessageRepository(ApplicationDbContext applicationDbContext) =>
            this.applicationDbContext = applicationDbContext;

        public async ValueTask<Message> InsertMessageAsync(Message message)
        {
            await this.applicationDbContext.Messages.AddAsync(message);
            await this.applicationDbContext.SaveChangesAsync();

            return message;
        }

        public IQueryable<Message> SelectAllMessages() =>
            this.applicationDbContext.Messages;

        public async ValueTask<Message> SelectMessageByIdAsync(Guid messageId) =>
            await this.applicationDbContext.Messages
            .FirstOrDefaultAsync(message => message.Id == messageId);

        public async ValueTask<Message> UpdateMessageAsync(Message message)
        {
            this.applicationDbContext.Entry(message).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();
            
            return message;
        }

        public async ValueTask<Message> DeleteMessageAsync(Message message)
        {
            this.applicationDbContext.Messages.Remove(message);
            await this.applicationDbContext.SaveChangesAsync();

            return message;
        }

        public async ValueTask RemoveRangeAsync(List<Message> messages)
        {
            this.applicationDbContext.Messages.RemoveRange(messages);

            await this.applicationDbContext.SaveChangesAsync();
        }
    }
}
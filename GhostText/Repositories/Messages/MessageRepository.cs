using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GhostText.Data;
using GhostText.Models;

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
        public async ValueTask<int> RemoveRangeAsync()
        {
            var oldMessage = this.applicationDbContext.Messages
               // .Where(message => message.CreateDate < DateTime.UtcNow.AddDays(-3));
               .Where(message => message.CreateDate < DateTime.UtcNow.AddMinutes(-1));
            this.applicationDbContext.Messages.RemoveRange(oldMessage);
            return await this.applicationDbContext.SaveChangesAsync();
        }
    }
}
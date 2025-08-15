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

        public MessageRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async ValueTask<Message> InsertMessageAsync(Message message)
        {
            this.applicationDbContext.Entry(message).State = EntityState.Added;
            await this.applicationDbContext.SaveChangesAsync();

            return message;
        }

        public IQueryable<Message> SelectAllMessages()
        {
            return this.applicationDbContext.Messages;
        }

        public async ValueTask<Message> SelectMessageByIdAsync(Guid messageId)
        {
            return await this.applicationDbContext.Messages.FirstOrDefaultAsync(
                message => message.Id == messageId);
        }

        public async ValueTask<Message> UpdateMessageAsync(Message message)
        {
            this.applicationDbContext.Entry(message).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();
            
            return message;
        }

        public async ValueTask<Message> DeleteMessageAsync(Message message)
        {
            this.applicationDbContext.Entry(message).State = EntityState.Deleted;
            await this.applicationDbContext.SaveChangesAsync();

            return message;
        }
    }
}
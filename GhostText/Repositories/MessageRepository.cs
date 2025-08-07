using System.Threading.Tasks;
using GhostText.Data;
using GhostText.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace GhostText.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public MessageRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<Message> InsertMessageAsync(Message message)
        {
            this.applicationDbContext.Entry(message).State = EntityState.Added;
            await this.applicationDbContext.SaveChangesAsync();

            return message;
        }

        public IQueryable<Message> SelectAllMessages()
        {
            return this.applicationDbContext.Messages;
        }

        public async Task<Message> SelectMessageByIdAsync(Guid messageId)
        {
            return await this.applicationDbContext.Messages.FirstOrDefaultAsync(
                message => message.Id == messageId);
        }

        public async Task<Message> UpdateMessageAsync(Message message)
        {
            this.applicationDbContext.Entry(message).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();

            return message;
        }
    }
}
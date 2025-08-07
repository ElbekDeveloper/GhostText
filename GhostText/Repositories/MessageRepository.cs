using GhostText.Data;
using GhostText.Models;
using Microsoft.EntityFrameworkCore;

namespace GhostText.Repositories
{
    public class MessageRepository:IMessageRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public MessageRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<Message> InsertMessageAsync(Message message)
        {
            await this.applicationDbContext.AddAsync(message);
            await this.applicationDbContext.SaveChangesAsync();
            return message;
        }

        public async Task<Message> SelectMessageByIdAsync(Guid messageId)
        { 
           return await this.applicationDbContext.Students.FirstOrDefaultAsync(
               message => message.Id == messageId);     
        }
    }
}


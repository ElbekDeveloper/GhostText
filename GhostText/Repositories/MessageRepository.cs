using System.Threading.Tasks;
using GhostText.Data;
using GhostText.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        public async Task<Message> UpdateMessageAsync(Message message)
        {
            this.applicationDbContext.Entry(message).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();

            return message;
        }
    }
}
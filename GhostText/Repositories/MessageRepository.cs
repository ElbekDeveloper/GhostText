using GhostText.Data;
using GhostText.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            await this.applicationDbContext.AddAsync(message);
            await this.applicationDbContext.SaveChangesAsync();

            return message;
        }

        public IQueryable<Message> SelectAllMessages()
        {
            return this.applicationDbContext.Messages;
        }

        public async Task<Message> UpdateMessageAsync(Message message)
        {
            this.applicationDbContext.Entry(message).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();

            return message;
        }
    }
}
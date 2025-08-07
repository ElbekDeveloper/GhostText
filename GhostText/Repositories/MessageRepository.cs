using System.Threading.Tasks;
using GhostText.Data;
using GhostText.Models;

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

        public async Task<Message> DeleteMessageAsync(Message message)
        {
            this.applicationDbContext.Remove(message);
            await this.applicationDbContext.SaveChangesAsync();
            
            return message;
        }
    }
}


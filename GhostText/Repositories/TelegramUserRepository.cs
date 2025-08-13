using GhostText.Data;
using GhostText.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GhostText.Repositories
{
    public class TelegramUserRepository : ITelegramUserRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public TelegramUserRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<TelegramUser> InsertTelegramUserAsync(TelegramUser telegramUser)
        {
            this.applicationDbContext.Entry(telegramUser).State = EntityState.Added;
            await this.applicationDbContext.SaveChangesAsync();

            return telegramUser;
        }

        public async Task<TelegramUser> UpdateTelegramUserAsync(TelegramUser telegramUser)
        {
            this.applicationDbContext.Entry(telegramUser).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();

            return telegramUser;
        }
    }
}

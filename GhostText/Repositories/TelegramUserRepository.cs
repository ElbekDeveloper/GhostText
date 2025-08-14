using System;
using GhostText.Data;
using GhostText.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        
        public IQueryable<TelegramUser> SelectAllTelegramUser()
        {
            return this.applicationDbContext.TelegramUsers;
        }

        public async Task<TelegramUser> SelectTelegramUserByIdAsync(Guid userId)
        {
            return await this.applicationDbContext.TelegramUsers.FirstOrDefaultAsync(
                user => user.Id == userId);
        }

        public async Task<TelegramUser> UpdateTelegramUserAsync(TelegramUser telegramUser)
        {
            this.applicationDbContext.Entry(telegramUser).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();

            return telegramUser;
        }
    }
}

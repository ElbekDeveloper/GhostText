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

        public TelegramUserRepository(ApplicationDbContext applicationDbContext) =>
            this.applicationDbContext = applicationDbContext;

        public async ValueTask<TelegramUser> InsertTelegramUserAsync(TelegramUser telegramUser)
        {
            this.applicationDbContext.Entry(telegramUser).State = EntityState.Added;
            await this.applicationDbContext.SaveChangesAsync();

            return telegramUser;
        }
        
        public IQueryable<TelegramUser> SelectAllTelegramUser() =>
            this.applicationDbContext.TelegramUsers;

        public async ValueTask<TelegramUser> SelectTelegramUserByIdAsync(Guid userId) =>
            await this.applicationDbContext.TelegramUsers
            .FirstOrDefaultAsync(user => user.Id == userId);
        
        public async ValueTask<TelegramUser> UpdateTelegramUserAsync(TelegramUser telegramUser)
        {
            this.applicationDbContext.Entry(telegramUser).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();

            return telegramUser;
        }
        
        public async ValueTask<TelegramUser> DeleteTelegramUserAsync(TelegramUser telegramUser)
        {
            this.applicationDbContext.TelegramUsers.Remove(telegramUser);
            await this.applicationDbContext.SaveChangesAsync();

            return telegramUser;
        }       
    }
}

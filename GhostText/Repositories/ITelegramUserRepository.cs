using System;
using GhostText.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Repositories
{
    public interface ITelegramUserRepository
    {
        Task<TelegramUser> InsertTelegramUserAsync(TelegramUser telegramUser);
        IQueryable<TelegramUser> SelectAllTelegramUser();
        Task<TelegramUser> SelectTelegramUserById(Guid userId);
        Task<TelegramUser> DeleteTelegramUserAsync(TelegramUser telegramUser);
    }
}

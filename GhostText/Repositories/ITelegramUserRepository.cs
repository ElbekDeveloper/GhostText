using System;
using GhostText.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Repositories
{
    public interface ITelegramUserRepository
    {
        ValueTask<TelegramUser> InsertTelegramUserAsync(TelegramUser telegramUser);
        IQueryable<TelegramUser> SelectAllTelegramUser();
        ValueTask<TelegramUser> SelectTelegramUserByIdAsync(Guid userId);
        ValueTask<TelegramUser> UpdateTelegramUserAsync(TelegramUser telegramUser);
        ValueTask<TelegramUser> DeleteTelegramUserAsync(TelegramUser telegramUser);
    }
}

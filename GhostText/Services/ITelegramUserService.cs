using System;
using GhostText.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Services
{
    public interface ITelegramUserService
    {
        ValueTask<TelegramUser> AddTelegramUserAsync(TelegramUser telegramUser);
        IQueryable<TelegramUser> RetrieveAllTelegramUser();
        ValueTask<TelegramUser> RetrieveTelegramUserByIdAsync(Guid userId);
        ValueTask<TelegramUser> ModifyTelegramUserAsync(TelegramUser telegramUser);
        ValueTask<TelegramUser> RemoveTelegramUserAsync(Guid userId);
        ValueTask<TelegramUser> EnsureTelegramUserAsync(TelegramUser telegramUser);
     }
}
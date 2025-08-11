using System;
using GhostText.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Services
{
    public interface ITelegramUserService
    {
        Task<TelegramUser> AddTelegramUserAsync(TelegramUser telegramUser);
        IQueryable<TelegramUser> RetrieveAllTelegramUser();
        Task<TelegramUser> RetrieveTelegramUserByIdAsync(Guid userId);
        Task<TelegramUser> RemoveTelegramUserAsync(Guid userId);
    }
}

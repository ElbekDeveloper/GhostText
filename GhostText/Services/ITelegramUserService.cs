using GhostText.Models;
using System.Threading.Tasks;

namespace GhostText.Services
{
    public interface ITelegramUserService
    {
        Task<TelegramUser> AddTelegramUserAsync(TelegramUser telegramUser);
        Task<TelegramUser> ModifyTelegramUserAsync(TelegramUser telegramUser);
    }
}

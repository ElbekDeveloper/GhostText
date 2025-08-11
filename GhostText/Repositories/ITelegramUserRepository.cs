using GhostText.Models;
using System.Threading.Tasks;

namespace GhostText.Repositories
{
    public interface ITelegramUserRepository
    {
        Task<TelegramUser> InsertTelegramUserAsync(TelegramUser telegramUser);
        Task<TelegramUser> UpdateTelegramUserAsync(TelegramUser telegramUser);
    }
}

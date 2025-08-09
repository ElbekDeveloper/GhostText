using GhostText.Models;
using System.Threading.Tasks;

namespace GhostText.Services
{
    public class TelegramUserService : ITelegramUserService
    {
        private readonly ITelegramUserService telegramUserRepository;

        public TelegramUserService(ITelegramUserService telegramUserRepository)
        {
            this.telegramUserRepository = telegramUserRepository;
        }

        public async Task<TelegramUser> AddTelegramUserAsync(TelegramUser telegramUser)
        {
            return await this.telegramUserRepository.AddTelegramUserAsync(telegramUser);
        }
    }
}

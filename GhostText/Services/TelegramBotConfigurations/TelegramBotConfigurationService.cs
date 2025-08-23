using GhostText.Models.TelegramBotConfigurations;
using GhostText.Repositories.TelegramBotConfigurations;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Services.TelegramBotConfigurations
{
    public class TelegramBotConfigurationService : ITelegramBotConfigurationService
    {
        private readonly ITelegramBotConfigurationRepository telegramBotConfigurationRepository;

        public TelegramBotConfigurationService(ITelegramBotConfigurationRepository telegramBotConfigurationRepository) =>
            this.telegramBotConfigurationRepository = telegramBotConfigurationRepository;

        public async ValueTask<TelegramBotConfiguration> AddTelegramBotConfigurationAsync(TelegramBotConfiguration configuration) =>
            await this.telegramBotConfigurationRepository.InsertChannelAsync(configuration);

        public IQueryable<TelegramBotConfiguration> RetrieveAllTelegramBotConfigurations()=>
            this.telegramBotConfigurationRepository.SelectAlltelegramBotConfigurations();
    }
}

using GhostText.Models.TelegramBotConfiguration;
using GhostText.Repositories.TelegramBotConfigurations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GhostText.Services.TelegramBotConfigurations
{
    public class TelegramBotConfigurationService : ITelegramBotConfigurationService
    {
        private readonly ITelegramBotConfigurationRepository telegramBotConfigurationRepository;

        public TelegramBotConfigurationService(ITelegramBotConfigurationRepository telegramBotConfigurationRepository) =>
            this.telegramBotConfigurationRepository = telegramBotConfigurationRepository;

        public async ValueTask<TelegramBotConfiguration> InsertChannelAsync(TelegramBotConfiguration configuration) =>
            await this.telegramBotConfigurationRepository.InsertChannelAsync(configuration);

        public async ValueTask<TelegramBotConfiguration> SelectChannelByIdAsync(long channelId) 
        {
            TelegramBotConfiguration configuration = 
                await this.telegramBotConfigurationRepository.SelectChannelByIdAsync(channelId);

            if (configuration is null)
                throw new KeyNotFoundException($"Telegram bot configuration with ID {channelId} not found.");

            return configuration;
        }
    }
}

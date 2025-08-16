using GhostText.Models.TelegramBotConfiguration;
using System.Threading.Tasks;

namespace GhostText.Repositories.TelegramBotConfigurations
{
    public interface ITelegramBotConfigurationRepository
    {
        ValueTask<TelegramBotConfiguration> InsertAsync(TelegramBotConfiguration configuration);
        ValueTask<TelegramBotConfiguration> SelectByChannelIdAsync(long channelId);
        ValueTask<TelegramBotConfiguration> SelectByTokenAsync(string token);
    }
}

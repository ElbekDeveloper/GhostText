using GhostText.Models.TelegramBotConfiguration;
using System.Threading.Tasks;

namespace GhostText.Services.TelegramBotConfigurations
{
    public interface ITelegramBotConfigurationService
    {
        ValueTask<TelegramBotConfiguration> InsertChannelAsync(TelegramBotConfiguration configuration);
        ValueTask<TelegramBotConfiguration> SelectChannelByIdAsync(long channelId);
    }
}

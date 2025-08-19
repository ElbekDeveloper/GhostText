using GhostText.Models.TelegramBotConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Repositories.TelegramBotConfigurations
{
    public interface ITelegramBotConfigurationRepository
    {
        ValueTask<TelegramBotConfiguration> InsertChannelAsync(TelegramBotConfiguration configuration);
        ValueTask<TelegramBotConfiguration> SelectChannelByIdAsync(long channelId);
        IQueryable<TelegramBotConfiguration> SelectAlltelegramBotConfigurations();
    }
}

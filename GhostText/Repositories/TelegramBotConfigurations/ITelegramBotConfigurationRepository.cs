using GhostText.Models.TelegramBotConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Repositories.TelegramBotConfigurations
{
    public interface ITelegramBotConfigurationRepository
    {
        ValueTask<TelegramBotConfiguration> InsertChannelAsync(TelegramBotConfiguration configuration);
        IQueryable<TelegramBotConfiguration> SelectAlltelegramBotConfigurations();
    }
}

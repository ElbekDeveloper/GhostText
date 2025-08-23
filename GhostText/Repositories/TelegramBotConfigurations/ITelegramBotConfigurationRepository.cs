using System.Linq;
using System.Threading.Tasks;
using GhostText.Models.TelegramBotConfigurations;

namespace GhostText.Repositories.TelegramBotConfigurations
{
    public interface ITelegramBotConfigurationRepository
    {
        ValueTask<TelegramBotConfiguration> InsertChannelAsync(TelegramBotConfiguration configuration);
        IQueryable<TelegramBotConfiguration> SelectAlltelegramBotConfigurations();
    }
}

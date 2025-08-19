using System.Linq;
using GhostText.Models.TelegramBotConfiguration;
using System.Threading.Tasks;

namespace GhostText.Services.TelegramBotConfigurations
{
    public interface ITelegramBotConfigurationService
    {
        ValueTask<TelegramBotConfiguration> AddTelegramBotConfigurationAsync(TelegramBotConfiguration configuration);
        IQueryable<TelegramBotConfiguration> RetrieveAllTelegramBotConfigurations();
    }
}

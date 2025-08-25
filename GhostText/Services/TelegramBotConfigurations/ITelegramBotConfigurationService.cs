using System.Linq;
using System.Threading.Tasks;
using GhostText.Models.TelegramBotConfigurations;

namespace GhostText.Services.TelegramBotConfigurations
{
    public interface ITelegramBotConfigurationService
    {
        ValueTask<TelegramBotConfiguration> AddTelegramBotConfigurationAsync(TelegramBotConfiguration configuration);
        IQueryable<TelegramBotConfiguration> RetrieveAllTelegramBotConfigurations();
    }
}

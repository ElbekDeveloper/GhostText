using System.Threading.Tasks;
using Coravel.Invocable;
using GhostText.Services.TelegramBotConfigurations;

namespace GhostText.Services.TelegramBotListeners;

public class TelegramBotListenersService : IInvocable
{
    private readonly ITelegramBotConfigurationService  telegramBotConfigurationService;

    public TelegramBotListenersService(ITelegramBotConfigurationService telegramBotConfigurationService) =>
        this.telegramBotConfigurationService = telegramBotConfigurationService;

    public Task Invoke() 
    {
        this.telegramBotConfigurationService.RetrieveAllTelegramBotConfigurations();
        
        return Task.CompletedTask;
    }
}
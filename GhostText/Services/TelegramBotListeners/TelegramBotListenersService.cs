using System;
using System.Linq;
using System.Threading.Tasks;
using Coravel.Invocable;
using GhostText.Models.TelegramBotConfiguration;
using GhostText.Services.TelegramBotConfigurations;
using GhostText.Clients.TelegramClients;
using System.Collections.Generic;

namespace GhostText.Services.TelegramBotListeners;

public class TelegramBotListenersService : IInvocable
{
    private readonly ITelegramBotConfigurationService  telegramBotConfigurationService;

    private static Dictionary<Guid, ITelegramClient> telegramClientsDictionary = 
        new Dictionary<Guid, ITelegramClient>();

    public TelegramBotListenersService(ITelegramBotConfigurationService telegramBotConfigurationService) =>
        this.telegramBotConfigurationService = telegramBotConfigurationService;

    public Task Invoke() 
    {
        List<Guid> isActiveTelegramBotList = telegramClientsDictionary.Keys.ToList();

        IQueryable<TelegramBotConfiguration> telegramBotList = 
            this.telegramBotConfigurationService.RetrieveAllTelegramBotConfigurations()
                .Where(bot => isActiveTelegramBotList.Contains(bot.Id) == false);

        foreach (TelegramBotConfiguration bot in telegramBotList)
        {
           var telegramClient = new TelegramClient(
                botToken: bot.Token,
                channelId: bot.ChannelId,
                serviceProvider: null);

            telegramClient.ListenTelegramBot();
            telegramClientsDictionary.Add(bot.Id, telegramClient);
        }

        return Task.CompletedTask;
    }
}
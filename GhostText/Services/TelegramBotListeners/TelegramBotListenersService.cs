using Coravel.Invocable;
using GhostText.Clients.TelegramClients;
using GhostText.Models.TelegramBotConfiguration;
using GhostText.Services.TelegramBotConfigurations;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Services.TelegramBotListeners;

public class TelegramBotListenersService : IInvocable
{
    private readonly ITelegramBotConfigurationService  telegramBotConfigurationService;
    private readonly IServiceProvider serviceProvider;

    private static Dictionary<Guid, ITelegramClient> telegramClientsDictionary = 
        new Dictionary<Guid, ITelegramClient>();

    public TelegramBotListenersService(
        ITelegramBotConfigurationService telegramBotConfigurationService,
        IServiceProvider serviceProvider)
    {
        this.telegramBotConfigurationService = telegramBotConfigurationService;
        this.serviceProvider = serviceProvider;
    }

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
                serviceProvider: this.serviceProvider);

            telegramClient.ListenTelegramBot();
            telegramClientsDictionary.Add(bot.Id, telegramClient);
        }

        return Task.CompletedTask;
    }
}
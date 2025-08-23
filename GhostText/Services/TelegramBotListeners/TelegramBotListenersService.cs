using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GhostText.Clients.TelegramClients;
using GhostText.Services.TelegramBotConfigurations;
using Coravel.Invocable;
using GhostText.Models.TelegramBotConfigurations;

namespace GhostText.Services.TelegramBotListeners;

public class TelegramBotListenersService : IInvocable
{
    private readonly ITelegramBotConfigurationService  telegramBotConfigurationService;
    private readonly ITelegramUserService telegramUserService;

    private static Dictionary<Guid, ITelegramClient> telegramClientsDictionary = 
        new Dictionary<Guid, ITelegramClient>();

    public TelegramBotListenersService(
        ITelegramBotConfigurationService telegramBotConfigurationService, 
        ITelegramUserService telegramUserService)
    {
        this.telegramBotConfigurationService = telegramBotConfigurationService;
        this.telegramUserService = telegramUserService;
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
                telegramUserService: telegramUserService);

            telegramClient.ListenTelegramBot();
            telegramClientsDictionary.Add(bot.Id, telegramClient);
        }

        return Task.CompletedTask;
    }
}
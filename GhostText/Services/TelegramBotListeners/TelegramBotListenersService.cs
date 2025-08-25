using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GhostText.Clients.TelegramClients;
using GhostText.Services.TelegramBotConfigurations;
using Coravel.Invocable;
using GhostText.Models.TelegramBotConfigurations;
using GhostText.Services.Requests;

namespace GhostText.Services.TelegramBotListeners;

public class TelegramBotListenersService : IInvocable
{
    private readonly ITelegramBotConfigurationService  telegramBotConfigurationService;
    private readonly ITelegramUserService telegramUserService;
    private readonly IRequestService requestService;

    private static Dictionary<Guid, ITelegramClient> telegramClientsDictionary = 
        new Dictionary<Guid, ITelegramClient>();

    public TelegramBotListenersService(
        ITelegramBotConfigurationService telegramBotConfigurationService,
        ITelegramUserService telegramUserService,
        IRequestService requestService)
    {
        this.telegramBotConfigurationService = telegramBotConfigurationService;
        this.telegramUserService = telegramUserService;
        this.requestService = requestService;
    }

    public Task Invoke() 
    {
        List<Guid> isActiveTelegramBotList = telegramClientsDictionary.Keys.ToList();

        IQueryable<TelegramBotConfiguration> telegramBotList = 
            this.telegramBotConfigurationService.RetrieveAllTelegramBotConfigurations()
                .Where(bot => isActiveTelegramBotList.Contains(bot.Id) == false);

        foreach (TelegramBotConfiguration bot in telegramBotList)
        {
            TelegramClient telegramClient = new TelegramClient(
                botToken: bot.Token,
                channelId: bot.ChannelId,
                telegramUserService: telegramUserService,
                requestService: requestService);

            telegramClient.ListenTelegramBot();
            telegramClientsDictionary.Add(bot.Id, telegramClient);
        }

        return Task.CompletedTask;
    }
}
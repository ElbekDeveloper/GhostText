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
    private readonly ITelegramBotConfigurationService telegramBotConfigurationService;
    private readonly ITelegramUserService telegramUserService;
    private readonly IRequestService requestService;
    private readonly IMessageService messageService;
    private Dictionary<Guid, ITelegramClient> telegramClientsDictionary;

    public TelegramBotListenersService(
        ITelegramUserService telegramUserService,
        IRequestService requestService,
        ITelegramBotConfigurationService telegramBotConfigurationService,
        IMessageService messageService)
    {
        this.telegramBotConfigurationService = telegramBotConfigurationService;
        this.telegramUserService = telegramUserService;
        this.requestService = requestService;
        this.messageService = messageService;
        this.telegramClientsDictionary = new Dictionary<Guid, ITelegramClient>();
    }

    public Task Invoke()
    {
        List<Guid> isActiveTelegramBotList =
            this.telegramClientsDictionary.Keys.ToList();

        IQueryable<TelegramBotConfiguration> telegramBotList =
            this.telegramBotConfigurationService.RetrieveAllTelegramBotConfigurations()
                .Where(bot => isActiveTelegramBotList.Contains(bot.Id) == false);

        foreach (TelegramBotConfiguration bot in telegramBotList)
        {
            TelegramClient telegramClient = new TelegramClient(
                botToken: bot.Token,
                channelId: bot.ChannelId,
                messageService: messageService,
                telegramUserService: telegramUserService,
                requestService: requestService,
                telegramBotConfigurationId: bot.Id);

            telegramClient.ListenTelegramBot();
            this.telegramClientsDictionary.Add(bot.Id, telegramClient);
        }

        return Task.CompletedTask;
    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GhostText.Clients.TelegramClients;
using GhostText.Models.TelegramBotConfiguration;
using GhostText.Services.TelegramBotConfigurations;
using Coravel.Invocable;

namespace GhostText.Services.TelegramBotListeners;

public class TelegramBotListenersService : IInvocable
{
    private readonly ITelegramBotConfigurationService  telegramBotConfigurationService;
    private readonly ITelegramUserService telegramUserService;
    private readonly IMessageService messageService;

    private static Dictionary<Guid, ITelegramClient> telegramClientsDictionary = 
        new Dictionary<Guid, ITelegramClient>();

    public TelegramBotListenersService(
        ITelegramBotConfigurationService telegramBotConfigurationService,
        ITelegramUserService telegramUserService,
        IMessageService messageService)
    {
        this.telegramBotConfigurationService = telegramBotConfigurationService;
        this.telegramUserService = telegramUserService;
        this.messageService = messageService;
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
                messageService: messageService,
                telegramUserService: telegramUserService);

            telegramClient.ListenTelegramBot();
            telegramClientsDictionary.Add(bot.Id, telegramClient);
        }

        return Task.CompletedTask;
    }
}
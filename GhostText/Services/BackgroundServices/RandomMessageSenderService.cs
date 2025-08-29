using Coravel.Invocable;
using GhostText.Models;
using GhostText.Models.TelegramBotConfigurations;
using GhostText.Services.TelegramBotConfigurations;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace GhostText.Services.BackgroundServices
{
    public class RandomMessageSenderService : IInvocable
    {
        private readonly ITelegramBotConfigurationService telegramBotConfigurationService;
        private readonly IMessageService messageService;

        public RandomMessageSenderService(
            ITelegramBotConfigurationService telegramBotConfigurationService,
            IMessageService messageService)
        {
            this.telegramBotConfigurationService = telegramBotConfigurationService;
            this.messageService = messageService;
        }

        public async Task Invoke()
        {
            IQueryable<TelegramBotConfiguration> telegramBotConfigurations =
                this.telegramBotConfigurationService.RetrieveAllTelegramBotConfigurations();

            foreach (TelegramBotConfiguration telegramBotConfiguration in telegramBotConfigurations)
            {
                try
                {
                    Message randomMessage =
                        this.messageService.RetrieveAllMessages().Where(message =>
                            message.TelegramBotConfigurationId == telegramBotConfiguration.Id
                            && message.IsSent == false)
                                .OrderBy(message => Guid.NewGuid())
                                    .FirstOrDefault();

                    if (randomMessage is null)
                        continue;

                    var telegramBotClient = new TelegramBotClient(telegramBotConfiguration.Token);

                    await telegramBotClient.SendMessage(
                        chatId: telegramBotConfiguration.ChannelId,
                        text: randomMessage.Text);

                    randomMessage.IsSent = true;
                    randomMessage.SentAt = DateTimeOffset.UtcNow;
                    await this.messageService.ModifyMessageAsync(randomMessage);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using GhostText.Models;
using GhostText.Services;
using GhostText.Services.Requests;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GhostText.Clients.TelegramClients
{
    public class TelegramClient : ITelegramClient
    {
        private readonly Guid telegramBotConfigurationId;
        private readonly TelegramSettings telegramSettings;
        private readonly TelegramBotClient botClient;
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly ITelegramUserService telegramUserService;
        private readonly IRequestService requestService;
        private readonly IMessageService messageService;

        public TelegramClient(
            string botToken,
            long channelId,
            ITelegramUserService telegramUserService,
            IRequestService requestService,
            IMessageService messageService,
            Guid telegramBotConfigurationId)
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            this.telegramSettings = new TelegramSettings
            {
                ChannelId = channelId,
                BotToken = botToken
            };

            this.botClient = new TelegramBotClient(
                    token: this.telegramSettings.BotToken);
            this.telegramUserService = telegramUserService;
            this.messageService = messageService;
            this.requestService = requestService;
            this.telegramBotConfigurationId = telegramBotConfigurationId;
        }

        public void ListenTelegramBot()
        {
            this.botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                errorHandler: HandleErrorAsync,
                cancellationToken: this.cancellationTokenSource.Token);

            Console.WriteLine("Telegram bot ishlayapti...");
        }

        private async Task HandleUpdateAsync(
            ITelegramBotClient telegramBotClient, 
            Update update, 
            CancellationToken token)
        {
            if (update.Type is not UpdateType.Message) 
                return;
            
            if (string.IsNullOrWhiteSpace(update.Message.Text)) 
                return;

            TelegramUser telegramUser = new TelegramUser
            {
                TelegramId = update.Message.From.Id,
                UserName = update.Message.From.Username ?? "NoUsername",
            };

            telegramUser = await telegramUserService.EnsureTelegramUserAsync(telegramUser);

            string messageText = update.Message.Text;

            Models.Message telegramMessage = new Models.Message
            {
                Id = Guid.NewGuid(),
                Text = messageText,
                CreateDate = DateTime.UtcNow,
                TelegramBotConfigurationId = this.telegramBotConfigurationId
            };

            await messageService.AddMessageAsync(telegramMessage);

            if (messageText.StartsWith("/start"))
            {
                await telegramBotClient.SendMessage(
                    chatId: this.telegramSettings.ChannelId,
                    text: "Xush kelibsiz");  
            }
            else
            {
                if (messageText.Length > 120)
                {
                    await telegramBotClient.SendMessage(
                        chatId: update.Message.Chat.Id,
                        text: "we cannot sent your text, because your text's length is" +
                              "greater than 120 characters.Please try with less characters. ");
                }
                else if (requestService.ContainsForbiddenWord(messageText) is false)
                {
                    await telegramBotClient.SendMessage(
                        chatId: this.telegramSettings.ChannelId,
                        text: $"{messageText}");
                }
                else
                {
                    await telegramBotClient.SendMessage(
                        chatId: update.Message.Chat.Id,
                        text: "Your text has a forbidden word.");
                }
            }
        }

        private Task HandleErrorAsync(
            ITelegramBotClient telegramBotCLient, 
            Exception exception, 
            CancellationToken token)
        {
            Console.WriteLine($"Error: {exception.Message}");

            return Task.CompletedTask;
        }

        public void StopListening() =>
            this.cancellationTokenSource.Cancel();
    }
}

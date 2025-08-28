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

            this.botClient = new TelegramBotClient(this.telegramSettings.BotToken);
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
            if (update.Type != UpdateType.Message || update.Message is null)
                return;

            var message = update.Message;
            string text = message.Text;

            if (string.IsNullOrWhiteSpace(text))
                return;

            long targetChannelId = this.telegramSettings.ChannelId;

            if (text.StartsWith("/start", StringComparison.OrdinalIgnoreCase))
            {
                await telegramBotClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Xush kelibsiz!",
                    cancellationToken: token);
                return;
            }

            if (text.Length > 120)
            {
                await telegramBotClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Matn 120 belgidan uzun. Iltimos, qisqartirib yuboring.",
                    cancellationToken: token);
                return;
            }

            if (requestService.ContainsForbiddenWord(text))
            {
                await telegramBotClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Matnda taqiqlangan so‘z bor.",
                    cancellationToken: token);
                return;
            }

            bool isFromPrivate = message.Chat.Type == ChatType.Private;
            bool isSameChat = message.Chat.Id == targetChannelId;

            if (isFromPrivate || !isSameChat)
            {
                await telegramBotClient.SendMessage(
                    chatId: targetChannelId,
                    text: text,
                    cancellationToken: token);
            }

            bool isInbound;

            if (message.From is null)
                isInbound = true;
            else if (message.From.IsBot)
                isInbound = false;
            else
                isInbound = true;

            if (isInbound)
            {
                var telegramUser = new TelegramUser
                {
                    TelegramId = message.From!.Id,
                    UserName = message.From.Username ?? "NoUsername",
                };
                await telegramUserService.EnsureTelegramUserAsync(telegramUser);

                var telegramMessage = new Models.Message
                {
                    Id = Guid.NewGuid(),
                    Text = text,
                    CreateDate = DateTime.UtcNow,
                    TelegramBotConfigurationId = this.telegramBotConfigurationId,
                    ChatId = targetChannelId
                };

                await messageService.AddMessageAsync(telegramMessage);
            }
        }

        private Task HandleErrorAsync(
            ITelegramBotClient telegramBotClient,
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

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
            if (update.Type != UpdateType.Message || string.IsNullOrWhiteSpace(update.Message?.Text))
            {
                await telegramBotClient.SendMessage(
                   chatId: update.Message.Chat.Id,
                   text: "Iltimos menga matnli xabar yuboring.\nMatn uzunligi 15 va 120 belgi oralig'ida bo'lsin!",
                   cancellationToken: token);
            }

            var message = update.Message;
            string text = message.Text;

            if (message.Chat.Type is ChatType.Private)
            {
                return;
            }

            long targetChannelId = this.telegramSettings.ChannelId;

            if (text.StartsWith("/start", StringComparison.OrdinalIgnoreCase))
            {
                await telegramBotClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Xush kelibsiz!",
                    cancellationToken: token);

                return;
            }

            if (text.Length > 120 || text.Length < 15)
            {
                await telegramBotClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Matn uzunligi 15 va 120 belgi oralig'ida bo'lsin.",
                    cancellationToken: token);

                return;
            }

            if (requestService.ContainsForbiddenWord(text))
            {
                await telegramBotClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Matnda taqiqlangan so‘z bor. Iltimos qaytadan tekshiring!",
                    cancellationToken: token);

                return;
            }

            var telegramUser = new TelegramUser
            {
                Id = Guid.NewGuid(),
                TelegramId = message.Chat.Id,
                UserName = message.Chat.Username,
                FullName = $"{message.Chat.FirstName} {message.Chat.LastName}"
            };

            await telegramUserService.EnsureTelegramUserAsync(telegramUser);

            var telegramMessage = new Models.Message
            {
                Id = Guid.NewGuid(),
                Text = text,
                CreateDate = DateTimeOffset.UtcNow,
                TelegramBotConfigurationId = this.telegramBotConfigurationId
            };

            await messageService.AddMessageAsync(telegramMessage);
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

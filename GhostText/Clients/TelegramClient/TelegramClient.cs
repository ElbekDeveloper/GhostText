using System;
using GhostText.Models.TelegramUsers;
using System.Threading;
using System.Threading.Tasks;
using GhostText.Clients.TelegramClient;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Configuration;

namespace GhostText.TelegramClient
{
    public class TelegramClient : ITelegramClient
    {
        private readonly TelegramSettings telegramSettings;
        private readonly TelegramBotClient botClient;
        private readonly CancellationTokenSource cancellationTokenSource;

        public TelegramClient(IConfiguration configuration)
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            this.telegramSettings = new TelegramSettings();
            configuration.Bind(nameof(TelegramSettings), this.telegramSettings);

            this.botClient = new TelegramBotClient(
                token: this.telegramSettings.BotToken);
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

            string messageText = update.Message.Text;

            if (messageText.StartsWith("/start"))
            {
                await telegramBotClient.SendMessage(
                    chatId: this.telegramSettings.ChannelId,
                    text: "Xush kelibsiz");
            }
            else
            {
                await telegramBotClient.SendMessage(
                    chatId: this.telegramSettings.ChannelId,
                    text: $"Botdan kelgan xabar:\n\n {messageText}");
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

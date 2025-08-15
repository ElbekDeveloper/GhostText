using System;
using GhostText.Models;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GhostText.TelegramClient
{
    public class TelegramClient
    {
        private readonly TelegramSettings telegramSettings;
        private readonly TelegramBotClient botClient;
        private readonly CancellationTokenSource cts = new CancellationTokenSource();

        public TelegramClient(IOptions<TelegramSettings> options)
        {
            telegramSettings = options.Value;
            if (string.IsNullOrWhiteSpace(telegramSettings.BotToken))
                throw new InvalidOperationException("Telegram BotToken topilmadi (configdan o‘qilmadi).");

            botClient = new TelegramBotClient(telegramSettings.BotToken);
        }


        public void ListenTelegramBot()
        {
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                errorHandler: HandleErrorAsync,
                cancellationToken: cts.Token
                );

            Console.WriteLine("Telegram bot ishlayapti...");
        }

        private async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken token)
        {
            if (update.Type != UpdateType.Message)
                return;

            if (update.Message?.Text is null)
                return;

            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;

            if (messageText.StartsWith("/start"))
            {
                await bot.SendMessage(chatId, "Xush kelibsiz");
            }
            else
            {
                await bot.SendMessage(chatId, $"Xabarni telegram kanalga yuboring: {messageText}");
            }
        }

        private Task HandleErrorAsync(ITelegramBotClient bot, Exception exception, CancellationToken token)
        {
            Console.WriteLine($"Error: {exception.Message}");
            return Task.CompletedTask;
        }

        public void StopListening()
        {
            cts.Cancel();
        }
    }
}

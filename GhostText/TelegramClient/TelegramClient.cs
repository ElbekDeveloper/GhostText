using System;
using GhostText.Models;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Configuration;

namespace GhostText.TelegramClient
{
    public class TelegramClient
    {
        private readonly TelegramSettings telegramSettings;
        private readonly TelegramBotClient botClient;
        private readonly CancellationTokenSource cts = new CancellationTokenSource();
        private readonly IConfiguration configuration;

        public TelegramClient(IOptions<TelegramSettings> options, IConfiguration configuration)
        {
            telegramSettings = options.Value;
            if (string.IsNullOrWhiteSpace(telegramSettings.BotToken))
                throw new InvalidOperationException("Telegram BotToken topilmadi (configdan o‘qilmadi).");

            botClient = new TelegramBotClient(telegramSettings.BotToken);

            this.configuration = configuration;
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

            var chatId = -1002766816339; // update.Message.Chat.Id;
            var messageText = update.Message.Text;

            if (messageText.StartsWith("/start"))
            {
                await bot.SendMessage(chatId, "Xush kelibsiz");
            }
            else
            {
                await bot.SendMessage(chatId, $"Botdan kelgan xabar:\n\n {messageText}");
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

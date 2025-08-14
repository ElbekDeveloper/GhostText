using GhostText.Models;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace GhostText.TelegramClient
{
    public class TelegramClient
    {
        private readonly TelegramSettings telegramSettings;
        private readonly TelegramBotClient botClient;

        public TelegramClient(IOptions<TelegramSettings> options)
        {
            telegramSettings = options.Value;
            botClient = new TelegramBotClient(telegramSettings.BotToken);
        }

        //public void ListenTelegramBot()
        //{
        //    botClient.StartReceiving(
               
        //        );
        //}

    }
}

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostText.Data;
using GhostText.Models;
using GhostText.Repositories;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace GhostText.Services.BackgroundServices
{
    public class RandomMessageSenderService : BackgroundService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMessageRepository messageRepository;
        private readonly ITelegramBotClient telegramBotClient;
        private static readonly Random random = new Random();

        public RandomMessageSenderService(
            IMessageRepository messageRepository,
            ITelegramBotClient telegramBotClient,
            ApplicationDbContext dbContext)
        {
            this.messageRepository = messageRepository;
            this.telegramBotClient = telegramBotClient;
            this.dbContext = dbContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            long channelId = this.dbContext
                .TelegramBotConfigurations
                .Select(bot => bot.ChannelId)
                .FirstOrDefault();

            TimeSpan interval = TimeSpan.FromMinutes(96);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    System.Collections.Generic.List<Message> allMessages = 
                        this.messageRepository.SelectAllMessages().ToList();

                    if (!allMessages.Any())
                    {
                        Console.WriteLine("[RandomMessageSenderService] Hech qanday xabar topilmadi.");
                        await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                        continue;
                    }

                    if (allMessages.Any())
                    {
                        Message msg = allMessages[random.Next(allMessages.Count)];
                        await this.telegramBotClient.SendMessage(
                            chatId: channelId,
                            text: msg.Text,
                            cancellationToken: stoppingToken
                        );
                    }

                    await Task.Delay(interval, stoppingToken);
                }
                catch (TaskCanceledException)
                { }
                catch (Exception ex)
                {
                    Console.WriteLine($"[RandomMessageSenderService] Error: {ex.Message}");
                }
            }
        }
    }
}

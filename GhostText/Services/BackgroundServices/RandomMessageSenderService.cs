using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostText.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace GhostText.Services.BackgroundServices
{
    public class RandomMessageSenderService : BackgroundService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ITelegramBotClient telegramBotClient;

        public RandomMessageSenderService(
            ITelegramBotClient telegramBotClient,
            ApplicationDbContext dbContext)
        {
            this.telegramBotClient = telegramBotClient;
            this.dbContext = dbContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            long channelId = await this.dbContext.TelegramBotConfigurations
                .Select(b => b.ChannelId)
                .FirstOrDefaultAsync(stoppingToken);

            if (channelId == 0)
            {
                Console.WriteLine("[MessageSender] ChannelId topilmadi. Xizmat to'xtaydi.");
                return;
            }

            TimeSpan interval = TimeSpan.FromMinutes(96);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var selections = await this.dbContext.Messages
                        .Where(m => !m.IsSent)
                        .OrderBy(m => Guid.NewGuid())
                        .Select(m => new { m.Id, m.Text })
                        .Take(1)
                        .ToListAsync(stoppingToken);

                    if (selections.Count == 0)
                    {
                        Console.WriteLine("[MessageSender] Jo'natilmagan xabar topilmadi.");
                    }
                    else
                    {
                        foreach (var select in selections)
                        {
                            int affected = await this.dbContext.Messages
                                .Where(m => m.Id == select.Id && !m.IsSent)
                                .ExecuteUpdateAsync(s => s
                                    .SetProperty(m => m.IsSent, true)
                                    .SetProperty(m => m.SentAt, DateTime.UtcNow),
                                    stoppingToken);

                            if (affected == 0) continue;

                            bool sentOk = false;
                            try
                            {
                                await this.telegramBotClient.SendMessage(
                                    chatId: channelId,
                                    text: select.Text,
                                    cancellationToken: stoppingToken);

                                sentOk = true;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"[MessageSender] send failed: {ex.Message}");
                            }

                            if (!sentOk)
                            {
                                await this.dbContext.Messages
                                    .Where(m => m.Id == select.Id)
                                    .ExecuteUpdateAsync(s => s
                                        .SetProperty(m => m.IsSent, false)
                                        .SetProperty(m => m.SentAt, (DateTime?)null),
                                        stoppingToken);
                            }

                            await Task.Delay(interval, stoppingToken);
                        }
                    }
                }
                catch (TaskCanceledException) { }
                catch (Exception ex)
                {
                    Console.WriteLine($"[MessageSender] Error: {ex.Message}");
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }
            }
        }

    }
}

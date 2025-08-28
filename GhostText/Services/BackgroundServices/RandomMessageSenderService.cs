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
            TimeSpan intervalBetweenCycles = TimeSpan.FromMinutes(60);
            TimeSpan intervalBetweenSends = TimeSpan.FromSeconds(1.2);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var candidates = await dbContext.Messages
                        .Where(m => !m.IsSent && m.ChatId != 0)
                        .OrderBy(_ => Guid.NewGuid())
                        .Select(m => new {
                            m.Id,
                            m.Text,
                            m.ChatId,
                            m.TelegramBotConfigurationId
                        })
                        .Take(100)
                        .ToListAsync(stoppingToken);

                    if (candidates.Count == 0)
                    {
                        await Task.Delay(intervalBetweenCycles, stoppingToken);
                        continue;
                    }

                    var picks = candidates
                        .GroupBy(x => new { x.TelegramBotConfigurationId, x.ChatId })
                        .Select(g => g.First())
                        .ToList();

                    foreach (var select in picks)
                    {
                        int affected = await dbContext.Messages
                            .Where(m => m.Id == select.Id && !m.IsSent)
                            .ExecuteUpdateAsync(s => s
                                .SetProperty(m => m.IsSent, true)
                                .SetProperty(m => m.SentAt, DateTime.UtcNow),
                                stoppingToken);

                        if (affected == 0) continue;

                        bool sentOk = false;
                        try
                        {
                            string token = await dbContext.TelegramBotConfigurations
                                .Where(b => b.Id == select.TelegramBotConfigurationId)
                                .Select(b => b.Token)
                                .FirstAsync(stoppingToken);

                            TelegramBotClient client = new Telegram.Bot.TelegramBotClient(token);

                            await client.SendMessage(
                                chatId: select.ChatId,
                                text: select.Text ?? string.Empty,
                                cancellationToken: stoppingToken);

                            sentOk = true;
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine($"[MessageSender] send failed: {ex.Message} " +
                                $"(bot:{select.TelegramBotConfigurationId}, chat:{select.ChatId})");
                        }

                        if (!sentOk)
                        {
                            await dbContext.Messages
                                .Where(m => m.Id == select.Id)
                                .ExecuteUpdateAsync(s => s
                                    .SetProperty(m => m.IsSent, false)
                                    .SetProperty(m => m.SentAt, (DateTime?)null),
                                    stoppingToken);
                        }

                        await Task.Delay(intervalBetweenSends, stoppingToken);
                    }

                    await Task.Delay(intervalBetweenCycles, stoppingToken);
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

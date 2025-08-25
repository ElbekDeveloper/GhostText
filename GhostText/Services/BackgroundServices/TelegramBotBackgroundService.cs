using System;
using System.Threading;
using System.Threading.Tasks;
using GhostText.Repositories;
using Microsoft.Extensions.Hosting;

namespace GhostText.Services.TelegramBotBackgroundService
{
    public class TelegramBotBackgroundService : BackgroundService
    {
        private readonly IMessageRepository messageRepository;
        public TelegramBotBackgroundService(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //    var massege = this.messageService.RetrieveAllMessages()
            //        .First(massege => massege.CreateDate <= DateTime.UtcNow.AddDays(-3).Date);

            //    await this.messageService.RemoveMessageByIdAsync(massege.Id);
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await this.messageRepository.RemoveRangeAsync();

                    await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
                }
                catch (TaskCanceledException)
                {
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[MessageCleanupService] Error: {ex.Message}");
                }
            }
        }
    }
}

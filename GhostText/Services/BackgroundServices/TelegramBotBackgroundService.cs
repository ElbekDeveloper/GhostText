using GhostText.Data;
using GhostText.Models;
using GhostText.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Writers;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

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

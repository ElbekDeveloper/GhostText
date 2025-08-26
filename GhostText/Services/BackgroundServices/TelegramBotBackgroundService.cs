using System;
using System.Linq;
using System.Threading.Tasks;
using Coravel.Invocable;
using GhostText.Repositories;

namespace GhostText.Services.BackgroundServices;

public class TelegramBotBackgroundService:IInvocable
{
    private readonly IMessageRepository messageRepository;

    public TelegramBotBackgroundService(IMessageRepository messageRepository)
    {
        this.messageRepository = messageRepository;
    }

    public async Task Invoke()
    {
        try
        {
            var messages = this.messageRepository.SelectAllMessages();

            var oldMessages = messages
                .Where(m => m.CreateDate <= DateTime.UtcNow.AddDays(-3))
                .ToList();

            if (oldMessages.Any())
            {
                await this.messageRepository.RemoveRangeAsync(oldMessages);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[MessageCleanupInvocable] Xatolik: {ex.Message}");
        }
    }
}
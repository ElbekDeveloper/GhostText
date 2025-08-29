using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coravel.Invocable;
using GhostText.Models;
using GhostText.Repositories;

namespace GhostText.Services.BackgroundServices;

public class TelegramBotBackgroundService : IInvocable
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
            IQueryable<Message> messages = this.messageRepository.SelectAllMessages();

            List<Message> oldMessages = messages
                .Where(m => m.CreateDate <= DateTimeOffset.UtcNow.AddDays(-2))
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
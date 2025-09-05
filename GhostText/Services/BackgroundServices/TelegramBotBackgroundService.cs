using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coravel.Invocable;
using GhostText.Models;
using GhostText.Repositories;
using Microsoft.EntityFrameworkCore;

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

            List<Message> oldMessages = await messages
                .Where(m => m.CreateDate <= DateTimeOffset.UtcNow.AddMinutes(-2))
                .ToListAsync();

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
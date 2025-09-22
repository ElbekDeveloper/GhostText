using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coravel.Invocable;
using GhostText.Models;
using GhostText.Repositories;
using Microsoft.Extensions.Logging;

namespace GhostText.Services.BackgroundServices;

public class TelegramBotBackgroundService : IInvocable
{
    private readonly IMessageRepository messageRepository;
    private readonly ILogger<TelegramBotBackgroundService> logger;

    public TelegramBotBackgroundService(IMessageRepository messageRepository,ILogger<TelegramBotBackgroundService> logger)
    {
        this.messageRepository = messageRepository;
        this.logger = logger;   
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
            logger.LogError( ex.Message);
        }
    }
}
using System;
using GhostText.Models.TelegramBotConfigurations;

namespace GhostText.Models;

public class Message
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;

    public bool IsSent { get; set; } = false;
    public DateTime? SentAt { get; set; }

    public Guid TelegramBotConfigurationId { get; set; }
    public TelegramBotConfiguration TelegramBotConfiguration { get; set; }

    public long ChatId { get; set; }
}
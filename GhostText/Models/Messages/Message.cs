using System;
using GhostText.Models.TelegramBotConfigurations;

namespace GhostText.Models;

public class Message
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTimeOffset CreateDate { get; set; }
    public bool IsSent { get; set; } = false;
    public DateTimeOffset? SentAt { get; set; }

    public Guid TelegramBotConfigurationId { get; set; }
    public TelegramBotConfiguration TelegramBotConfiguration { get; set; }
}
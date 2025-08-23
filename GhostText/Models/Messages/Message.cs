using System;

namespace GhostText.Models;

public class Message
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreateDate { get; set; }

    public Guid TelegramBotConfigurationId { get; set; }
    public GhostText.Models.TelegramBotConfiguration.TelegramBotConfiguration TelegramBotConfiguration { get; set; }
}
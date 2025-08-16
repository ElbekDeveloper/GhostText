using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace GhostText.Models.TelegramBotConfiguration
{
    [Index(nameof(ChannelId), IsUnique = true)]
    [Index(nameof(Token), IsUnique = true)]
    public class TelegramBotConfiguration
    {
        public Guid Id { get; set; }
        public long ChannelId { get; set; }
        public string Token { get; set; }

    }
}

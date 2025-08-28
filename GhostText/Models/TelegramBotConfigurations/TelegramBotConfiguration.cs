using System;

namespace GhostText.Models.TelegramBotConfigurations
{
    public class TelegramBotConfiguration
    {
        public Guid Id { get; set; }
        public long ChannelId { get; set; }
        public string Token { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}

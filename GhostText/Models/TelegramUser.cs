using System;

namespace GhostText.Models
{
    public class TelegramUser
    {
        public Guid Id { get; set; } 
        public string UserName { get; set; }
        public string FullName { get; set; }
        public long TelegramId { get; set; }
    }
}

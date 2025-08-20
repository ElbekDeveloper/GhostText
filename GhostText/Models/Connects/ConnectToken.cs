using System;

namespace GhostText.Models.Connects;

public class ConnectToken
{
    public Guid Id { get; set; }
    public string BotTokenId { get; set; }
    public long ChanelId { get; set; }
}
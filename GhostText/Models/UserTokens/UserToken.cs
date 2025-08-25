using System;

namespace GhostText.Models.UserTokens
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace GhostText.Models.UserCredentials
{
    public class UserCredential
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

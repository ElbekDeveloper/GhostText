using GhostText.Models.UserCredentials;
using GhostText.Models.UserTokens;
using GhostText.Services.Accounts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GhostText.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountsController(IAccountService accountService) =>
            this.accountService = accountService;

        [HttpPost("api/login")]
        public async ValueTask<ActionResult<UserToken>> LoginAsync([FromBody] UserCredential userCredential) 
        {
            UserToken userToken = 
                await this.accountService.LoginAsync(userCredential);

            return Ok(userToken);
        }
    }
}

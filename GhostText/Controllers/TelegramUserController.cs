using GhostText.Models;
using GhostText.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GhostText.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramUserController : ControllerBase
    {
        private readonly ITelegramUserRepository AddTelegramUserAsync;

        public TelegramUserController(ITelegramUserRepository addTelegramUserAsync)
        {
            this.AddTelegramUserAsync = addTelegramUserAsync;
        }

        [HttpPost]
        public async Task<ActionResult<TelegramUser>> PostTelegramUserAsync(TelegramUser telegramUser)
        {
            await this.AddTelegramUserAsync.InsertTelegramUserAsync(telegramUser);
            return Ok(telegramUser);
        }
    }
}

using GhostText.Models;
using GhostText.Repositories;
using GhostText.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GhostText.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramUserController : ControllerBase
    {
        private readonly ITelegramUserService telegramUserService;

        public TelegramUserController(ITelegramUserService telegramUserService)
        {
            this.telegramUserService = telegramUserService;
        }

        [HttpPost]
        public async Task<ActionResult<TelegramUser>> PostTelegramUserAsync(TelegramUser telegramUser)
        {
            await this.telegramUserService.AddTelegramUserAsync(telegramUser);
            return Ok(telegramUser);
        }

        [HttpPut]
        public async Task<ActionResult<TelegramUser>> PutTelegramUserAsync(TelegramUser telegramUser)
        {
            if (telegramUser == null)
            {
                return BadRequest("Telegram user cannot be null.");
            }
            var updatedUser = await this.telegramUserService.ModifyTelegramUserAsync(telegramUser);
            if (updatedUser is null)
            {
                return NotFound("Telegram user not found.");
            }
            return Ok(updatedUser);
        }
    }
}

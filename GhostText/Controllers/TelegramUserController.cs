using GhostText.Models;
using GhostText.Repositories;
using GhostText.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramUserController : ControllerBase
    {
        private readonly ITelegramUserService telegramUserService;
        private readonly ITelegramUserService telegramUser;

        public TelegramUserController(ITelegramUserService telegramUserService)
        public TelegramUserController(ITelegramUserService telegramUser)
        {
            this.telegramUserService = telegramUserService;
            this.telegramUser = telegramUser;
        }

        [HttpPost]
        public async Task<ActionResult<TelegramUser>> PostTelegramUserAsync(TelegramUser telegramUser)
        {
            await this.telegramUserService.AddTelegramUserAsync(telegramUser);

            await this.telegramUser.AddTelegramUserAsync(telegramUser);
            return Ok(telegramUser);
        }

        [HttpGet]
        public IQueryable<TelegramUser> GetAllTelegramUsers()
        {
            IQueryable<TelegramUser> telegramUsers = this.telegramUser.RetrieveAllTelegramUser();

            return telegramUsers;
        }

        [HttpPut]
        public async Task<ActionResult<TelegramUser>> PutTelegramUserAsync(TelegramUser telegramUser)
        {
            if (telegramUser is null)
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

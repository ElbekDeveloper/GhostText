using System;
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
        private readonly ITelegramUserService telegramUser;

        public TelegramUserController(ITelegramUserService telegramUser)
        {
            this.telegramUser = telegramUser;
        }

        [HttpPost]
        public async Task<ActionResult<TelegramUser>> PostTelegramUserAsync(TelegramUser telegramUser)
        {
            await this.telegramUser.AddTelegramUserAsync(telegramUser);

            return Ok(telegramUser);
        }

        [HttpGet]
        public IQueryable<TelegramUser> GetAllTelegramUsers()
        {
            IQueryable<TelegramUser> telegramUsers = this.telegramUser.RetrieveAllTelegramUser();

            return telegramUsers;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<TelegramUser>> GetTelegramUserByIdAsync(Guid userId)
        {
           var result = await this.telegramUser.RetrieveTelegramUserByIdAsync(userId);

            if (result is null)
            {
                return NotFound($"Telegram user with ID {userId} not found.");
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<TelegramUser>> PutTelegramUserAsync(TelegramUser telegramUser)
        {
            if (telegramUser is null)
            {
                return BadRequest("Telegram user cannot be null.");
            }

            var updatedUser = await this.telegramUser.ModifyTelegramUserAsync(telegramUser);

            if (updatedUser is null)
            {
                return NotFound("Telegram user not found.");
            }

            return Ok(updatedUser);
        }


        [HttpDelete("{telegramUserId}")]
        public async Task<ActionResult<TelegramUser>> DeleteTelegramUserByIdAsync(Guid telegramUserId)
        {
            var result = await this.telegramUser.RemoveTelegramUserAsync(telegramUserId);

            if (result is null)
            {
                return NotFound($"Telegram user with ID {telegramUserId} not found.");
            }

            return Ok(result);
        }
    }
}

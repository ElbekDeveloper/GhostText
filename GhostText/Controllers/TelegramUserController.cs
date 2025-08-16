using System;
using GhostText.Models;
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

        public TelegramUserController(ITelegramUserService telegramUser) =>
            this.telegramUserService = telegramUser;

        [HttpPost]
        public async ValueTask<ActionResult<TelegramUser>> PostTelegramUserAsync(TelegramUser telegramUser) =>
            Ok(await this.telegramUserService.AddTelegramUserAsync(telegramUser));

        [HttpGet]
        public ActionResult<IQueryable<TelegramUser>> GetAllTelegramUsersAsync() =>
            Ok(this.telegramUserService.RetrieveAllTelegramUser().ToList());

        [HttpGet("{userId}")]
        public async ValueTask<ActionResult<TelegramUser>> GetTelegramUserByIdAsync(Guid userId) =>
           (await this.telegramUserService.RetrieveTelegramUserByIdAsync(userId)) is { } user 
            ? Ok(user) 
            : NotFound()

        [HttpPut]
        public async ValueTask<ActionResult<TelegramUser>> PutTelegramUserAsync(TelegramUser telegramUser)
        

        [HttpPut("{userId}")]
        public async ValueTask<ActionResult<TelegramUser>> PutTelegramUserAsync(Guid userId, TelegramUser telegramUser)
        {
            if (telegramUser is null)
                return BadRequest("Telegram user cannot be null.");

            telegramUser.Id = userId;

            TelegramUser updatedUser = await this.telegramUserService.ModifyTelegramUserAsync(telegramUser);

            return updatedUser is not null
                ? Ok(updatedUser)
                : NotFound($"Telegram user with ID {userId} not found.");
        }
        
        [HttpDelete("{telegramUserId}")]
        public async ValueTask<ActionResult<TelegramUser>> DeleteTelegramUserByIdAsync(Guid telegramUserId)
        {
            var result = await this.telegramUser.RemoveTelegramUserAsync(telegramUserId);

            if (result is null)
            {
                return NotFound($"Telegram user with ID {telegramUserId} not found.");
            }

            return Ok(result);
        }

        [HttpDelete("{telegramUserId}")]
        public async ValueTask<ActionResult<TelegramUser>> DeleteTelegramUserByIdAsync(Guid telegramUserId) =>
            (await this.telegramUserService.RemoveTelegramUserAsync(telegramUserId)) is { } user 
            ? Ok(user)
            : NoContent();
    }
}

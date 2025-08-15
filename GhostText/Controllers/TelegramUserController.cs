using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GhostText.Models;
using GhostText.Services;

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
        public async Task<ActionResult<TelegramUser>> PostTelegramUserAsync(TelegramUser telegramUser) =>
            Ok(await this.telegramUserService.AddTelegramUserAsync(telegramUser));

        [HttpGet]
        public ActionResult<IQueryable<TelegramUser>> GetAllTelegramUsersAsync() =>
            Ok(this.telegramUserService.RetrieveAllTelegramUser());

        [HttpGet("{userId}")]
        public async Task<ActionResult<TelegramUser>> GetTelegramUserByIdAsync(Guid userId) =>
           (await this.telegramUserService.RetrieveTelegramUserByIdAsync(userId)) is { } user 
            ? Ok(user) 
            : NotFound();

        [HttpPut("{userId}")]
        public async Task<ActionResult<TelegramUser>> PutTelegramUserAsync(Guid userId, TelegramUser telegramUser)
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
        public async Task<ActionResult<TelegramUser>> DeleteTelegramUserByIdAsync(Guid telegramUserId) =>
            (await this.telegramUserService.RemoveTelegramUserAsync(telegramUserId)) is { } user 
            ? Ok(user)
            : NoContent();
    }
}

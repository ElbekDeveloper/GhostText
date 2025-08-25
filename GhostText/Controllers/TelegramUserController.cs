using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GhostText.Models;
using GhostText.Services;
using Microsoft.AspNetCore.Authorization;

namespace GhostText.Controllers
{
    [ApiController, Authorize]
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
            Ok(await this.telegramUserService.RetrieveTelegramUserByIdAsync(userId));

        [HttpPut]
        public async ValueTask<ActionResult<TelegramUser>> PutTelegramUserAsync(TelegramUser telegramUser) =>
            Ok(await this.telegramUserService.ModifyTelegramUserAsync(telegramUser));

        [HttpDelete("{telegramUserId}")]
        public async ValueTask<ActionResult<TelegramUser>> DeleteTelegramUserByIdAsync(Guid telegramUserId) =>
            Ok(await this.telegramUserService.RemoveTelegramUserAsync(telegramUserId));
    }
}

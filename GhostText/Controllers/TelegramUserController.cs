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
    }
}

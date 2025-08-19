using GhostText.Models.TelegramBotConfiguration;
using GhostText.Services.TelegramBotConfigurations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GhostText.Controllers
{
    [ApiController]
    [Route("api/telegram-bot-configuration")]
    public class TelegramBotConfigurationController : ControllerBase
    {
        private readonly ITelegramBotConfigurationService telegramBotConfigurationService;

        public TelegramBotConfigurationController(ITelegramBotConfigurationService telegramBotConfigurationService) =>
            this.telegramBotConfigurationService = telegramBotConfigurationService;

        [HttpPost]
        public async ValueTask<ActionResult<TelegramBotConfiguration>> PostTelegramBotConfigurationAsync(
            TelegramBotConfiguration configuration) =>
            Ok(await this.telegramBotConfigurationService.InsertChannelAsync(configuration));
    }
}

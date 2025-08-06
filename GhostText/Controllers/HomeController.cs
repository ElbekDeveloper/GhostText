using GhostText.Models;
using GhostText.Services;
using Microsoft.AspNetCore.Mvc;

namespace GhostText.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IMessageService messageService;

        public HomeController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost]
        public async Task<Message> PostMessageAsync(Message message) 
        {
                await this.messageService.AddMessageAsync(message);
                return message;
        }

        [HttpGet]
        public IQueryable<Message> GetAllMessages()
        {
                IQueryable<Message> messages = this.messageService.RetrieveAllMessage();
                return messages;
        }
    }
}

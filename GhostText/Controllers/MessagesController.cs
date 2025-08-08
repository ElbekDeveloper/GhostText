using System;
using GhostText.Models;
using GhostText.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GhostText.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessagesController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost]
        public async Task<ActionResult<Message>> PostMessageAsync(Message message)
        {
           await this.messageService.AddMessageAsync(message);
           
           return Ok(message);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessageByIdAsync(Guid id)
        {
            var message = await this.messageService.RetrieveMessageByIdAsync(id);

            if (message is null)
            {
                return NotFound();
            }

            return Ok(message);
        }
    }
}

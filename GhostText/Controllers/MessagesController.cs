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

        [HttpGet("{messageId}")]
        public async Task<ActionResult<Message>> GetMessageByIdAsync(Guid messageId)
        {
            var message = await this.messageService.RetrieveMessageByIdAsync(messageId);

            if (message is null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        [HttpDelete("{messageId}")]
        public async Task<ActionResult<Message>> DeleteMessageByIdAsync(Guid messageId)
        {
            var message = await this.messageService.RemoveMessageByIdAsync(messageId);

            if(message is null)
            {
                return NotFound();
            }

            return Ok(message);
        }
    }
}

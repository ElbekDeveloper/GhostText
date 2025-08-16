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
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessagesController(IMessageService messageService) =>
            this.messageService = messageService;
           
        [HttpPost]
        public async ValueTask<ActionResult<Message>> PostMessageAsync(Message message) =>
            Ok(await this.messageService.AddMessageAsync(message));

        [HttpGet]
        public IQueryable<Message> GetAllMessages() =>
            this.messageService.RetrieveAllMessages();

        [HttpGet("{messageId}")]
        public async ValueTask<ActionResult<Message>> GetMessageByIdAsync(Guid messageId) =>
            await this.messageService.RetrieveMessageByIdAsync(messageId) is { } message 
            ? Ok(message) 
            : NotFound();

        [HttpPut("{messageId}")]
        public async ValueTask<ActionResult<Message>> PutMessageAsync(Guid messageId, Message message)
        {
            if (messageId != message.Id)
                return BadRequest("Message ID does not match");

            var updatedMessage = await this.messageService.ModifyMessageAsync(message);

            return updatedMessage is not null
                ? Ok(updatedMessage)
                : NotFound($"Message user with ID {messageId} not found.");
        }

        [HttpDelete("{messageId}")]
        public async ValueTask<ActionResult<Message>> DeleteMessageByIdAsync(Guid messageId) =>
            await this.messageService.RemoveMessageByIdAsync(messageId) is { } message
            ? Ok(message) 
            : NoContent();
    }
}

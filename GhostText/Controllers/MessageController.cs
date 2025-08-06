using GhostText.Models;
using GhostText.Services;
using Microsoft.AspNetCore.Mvc;

namespace GhostText.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
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

        [HttpGet("{id}")]
        public Task<Message> GetMessageByIdAsync(Guid messageId)
        {
            var message = this.messageService.RetrieveMessageByYdAsync(messageId);
            return message;
        }

        [HttpPut]
        public async Task<Message> PutMessageAsync(Message message)
        {
            await this.messageService.ModifyMessageAsync(message);
            return message;
        }

        [HttpDelete]
        public async Task<Message> DeleteMessageAsync(Guid messageId)
        {
            var message=await this.messageService.DeleteMessageAsync(messageId);
            return message;
        }
    }
}

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
        public ActionResult<IQueryable<Message>> GetAllMessages() =>
            Ok(this.messageService.RetrieveAllMessages().ToList());

        [HttpGet("{messageId}")]
        public async ValueTask<ActionResult<Message>> GetMessageByIdAsync(Guid messageId) =>
            Ok(await this.messageService.RetrieveMessageByIdAsync(messageId));
  
        [HttpPut]
        public async ValueTask<ActionResult<Message>> PutMessageAsync(Message message) =>
            Ok(await this.messageService.ModifyMessageAsync(message));

        [HttpDelete("{messageId}")]
        public async ValueTask<ActionResult<Message>> DeleteMessageByIdAsync(Guid messageId) =>
            Ok(await this.messageService.RemoveMessageByIdAsync(messageId));
    }
}

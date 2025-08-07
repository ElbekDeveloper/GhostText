using GhostText.Models;
using GhostText.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GhostText.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public async Task<Message> AddMessageAsync(Message message)
        {
            return await this.messageRepository.InsertMessageAsync(message);
        }

        public Task<Message> ModifyMessageAsync(Message message) 
        {
            var maybeMessage = this.messageRepository.SelectMessageByIdAsync(message.Id);
            if (maybeMessage == null)
            {
                throw new KeyNotFoundException($"Message with ID {message.Id} not found.");
            }

            return this.messageRepository.UpdateMessageAsync(message);
        }
    }
}
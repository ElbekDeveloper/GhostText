using System;
using System.Collections.Generic;
using GhostText.Models;
using System.Collections.Generic;
using GhostText.Repositories;
using System.Linq;
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

        public IQueryable<Message> RetrieveAllMessages()
        {
            return this.messageRepository.SelectAllMessages();
        }

        public async Task<Message> RetrieveMessageByIdAsync(Guid messageId)
        {
            var message = await this.messageRepository.SelectMessageByIdAsync(messageId);
            if (message == null)
            {
                throw new KeyNotFoundException($"Message with Id:{messageId} is not found");
            }

            return message;

        }

        public async Task<Message> ModifyMessageAsync(Message message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message), "Message cannot be null");
            }

            var existingMessage = await this.messageRepository.SelectMessageByIdAsync(message.Id);

            if (existingMessage is null)
            {
                throw new KeyNotFoundException("Message not found");
            }

            return await this.messageRepository.UpdateMessageAsync(message);
        }
    }
}

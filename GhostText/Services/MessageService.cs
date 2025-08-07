using System;
using System.Collections.Generic;
using GhostText.Models;
using GhostText.Repositories;
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

        public async Task<Message> RetrieveMessageByIdAsync(Guid messageId)
        {
            var message=await this.messageRepository.SelectMessageByIdAsync(messageId);
            if (message == null)
            {
                throw new KeyNotFoundException($"Message with Id:{messageId} is not found");
            }
            
            return message;
        }
    }
}
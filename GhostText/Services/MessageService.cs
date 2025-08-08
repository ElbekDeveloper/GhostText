using GhostText.Models;
using GhostText.Repositories;
using System;
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

        public async Task<Message> DeleteMessageAsync(Guid id)
        {
            var existingMessage = await this.messageRepository.SelectMessageByIdAsync(id);
            if (existingMessage is null)
            {
                throw new KeyNotFoundException("Message not found");
            }

            return await this.messageRepository.DeleteMessageAsync(existingMessage);
        }
    }
}
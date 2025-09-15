using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GhostText.Models;
using GhostText.Repositories;
using GhostText.Repositories.DateTimes;

namespace GhostText.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;
        private readonly IDateTimeRepository dateTimeRepository;

        public MessageService(
            IMessageRepository messageRepository, 
            IDateTimeRepository dateTimeRepository)
        {
            this.messageRepository = messageRepository;
            this.dateTimeRepository = dateTimeRepository;
        }

        public async ValueTask<Message> AddMessageAsync(Message message)
        {
            if (string.IsNullOrWhiteSpace(message.Text))
                throw new ArgumentException("Message text cannot be empty.");

            if(message.Text.Length > 120 || message.Text.Length < 15)
                throw new ArgumentException("Messge length should be between 15 and 120");

            message.CreateDate = this.dateTimeRepository.GetCurrentDateTime();

            return await this.messageRepository.InsertMessageAsync(message);
        }

        public IQueryable<Message> RetrieveAllMessages() =>
            this.messageRepository.SelectAllMessages();

        public async ValueTask<Message> RetrieveMessageByIdAsync(Guid messageId)
        {
            if(messageId == Guid.Empty)
                throw new ArgumentException("Message id is required.");

            Message maybeMessage =
                await this.messageRepository.SelectMessageByIdAsync(messageId);

            if(maybeMessage is null)
                throw new KeyNotFoundException($"Message is not found with given id: {messageId}.");

            return maybeMessage;
        }

        public async ValueTask<Message> ModifyMessageAsync(Message message)
        {
            if(message is null)
                throw new ArgumentException("Message is null.");

            Message maybeMessage =
                await this.messageRepository.SelectMessageByIdAsync(message.Id);

            if(maybeMessage is null)
                throw new KeyNotFoundException($"Message is not found with given id: {message.Id}.");

            return await this.messageRepository.UpdateMessageAsync(message);
        }

        public async ValueTask<Message> RemoveMessageByIdAsync(Guid messageId)
        {
            if (messageId == Guid.Empty)
                throw new ArgumentException("Message Id cannot be empty.");

            Message existingMessage = await this.messageRepository.SelectMessageByIdAsync(messageId);

            if (existingMessage is null)
                throw new KeyNotFoundException("Message not found");

            return await this.messageRepository.DeleteMessageAsync(existingMessage);
        }
    }
}

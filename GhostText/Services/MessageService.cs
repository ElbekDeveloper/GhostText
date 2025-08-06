using GhostText.Models;
using GhostText.Repositories;

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
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            await this.messageRepository.InsertMessageAsync(message);
            return message;
        }

        public IQueryable<Message> RetrieveAllMessage()
        {
            var messages = this.messageRepository.SelectAllMessages();
            return messages;
        }

        public async Task<Message> RetrieveMessageByYdAsync(Guid messageId)
        {
            Message message= await this.messageRepository.SelectMessageById(messageId);
            if (message == null)
            {
                throw new KeyNotFoundException($"Message with ID {messageId} not found");
            }
            
            return message;
        }

        public async Task<Message> ModifyMessageAsync(Message message)
        {
            Message maybeMessage=await this.messageRepository.SelectMessageById(message.Id);
            if (maybeMessage == null)
            {
                throw new KeyNotFoundException($"Message with ID {message.Id} not found");
            }
            
            await this.messageRepository.UpdateMessageAsync(message);
            return message;
        }

        public async Task<Message> DeleteMessageAsync(Guid messageId)
        {
            Message maybeMessage = await this.messageRepository.SelectMessageById(messageId);
            if (maybeMessage == null)
            {
                throw new KeyNotFoundException($"Message with ID {messageId} not found");
            }
            
            await this.messageRepository.DeleteMessageAsync(maybeMessage);
            return maybeMessage;
            
        }
    }
}

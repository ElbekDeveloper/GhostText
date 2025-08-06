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
    }
}

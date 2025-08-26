using Coravel.Invocable;
using GhostText.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GhostText.Services.Invocables
{
    public class MessageCleanupInvocable : IInvocable
    {
        private readonly IMessageRepository messageRepository;

        public MessageCleanupInvocable(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public async Task Invoke()
        {
            try
            {
                var messages = await this.messageRepository.GetAllAsync();

                var oldMessages = messages
                    .Where(m => m.CreateDate <= DateTime.UtcNow.AddDays(-3))
                    .ToList();

                if (oldMessages.Any())
                {
                    await this.messageRepository.RemoveRangeAsync(oldMessages);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MessageCleanupInvocable] Xatolik: {ex.Message}");
            }
        }
    }
}

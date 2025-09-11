using GhostText.Models;
using GhostText.Repositories;
using GhostText.Services;
using Moq;
using Tynamix.ObjectFiller;

namespace GhostText.Unit.Tests.Services.Messages
{
    public partial class MessageServiceTests
    {
        private readonly IMessageService messageService;
        private readonly Mock<IMessageRepository> messageRepositoryMock;

        public MessageServiceTests()
        {
            this.messageRepositoryMock = new Mock<IMessageRepository>();

            this.messageService = new MessageService(
                messageRepository: this.messageRepositoryMock.Object);
        }

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static string GetRandomValidText()
        {
            return new MnemonicString(
                  wordCount: 20, 
                  wordMinLength: 3,
                  wordMaxLength: 3
              ).GetValue();
        }

        private static Message CreateRandomMessage() =>
            CreateMessageFiller().Create();

        private static Filler<Message> CreateMessageFiller()
        {
            var filler = new Filler<Message>();

            filler.Setup()
                .OnProperty(message => message.Text).Use(GetRandomValidText)
                .OnProperty(message => message.TelegramBotConfiguration).IgnoreIt()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset)
                .OnType<DateTimeOffset?>().Use(GetRandomDateTimeOffset());

            return filler;
        }
    }
}

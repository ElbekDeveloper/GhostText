using GhostText.Models;
using GhostText.Repositories;
using GhostText.Repositories.DateTimes;
using GhostText.Services;
using Moq;
using Tynamix.ObjectFiller;

namespace GhostText.Unit.Tests.Services.Messages
{
    public partial class MessageServiceTests
    {
        private readonly IMessageService messageService;
        private readonly Mock<IMessageRepository> messageRepositoryMock;
        private readonly Mock<IDateTimeRepository> dateTimeRepositoryMock;

        public MessageServiceTests()
        {
            this.messageRepositoryMock = new Mock<IMessageRepository>();
            this.dateTimeRepositoryMock = new Mock<IDateTimeRepository>();

            this.messageService = new MessageService(
                messageRepository: this.messageRepositoryMock.Object,
                dateTimeRepository: this.dateTimeRepositoryMock.Object);
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

        public static TheoryData<string> CreateInvalidTexts()
        {
            string smallText = new MnemonicString(
                  wordCount: 3,
                  wordMinLength: 1,
                  wordMaxLength: 4
              ).GetValue();

            string largeText = new MnemonicString(
                  wordCount: 30,
                  wordMinLength: 4,
                  wordMaxLength: 5
              ).GetValue();

            return new TheoryData<string>
            {
                smallText,
                largeText
            };
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static IQueryable<Message> CreateRandomMessages()
        {
            int randomCount = GetRandomNumber();

            return CreateMessageFiller(GetRandomDateTimeOffset())
                .Create(randomCount)
                    .AsQueryable();
        }

        private static Message CreateRandomMessage(DateTimeOffset dates) =>
            CreateMessageFiller(dates).Create();

        private static Filler<Message> CreateMessageFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Message>();

            filler.Setup()
                .OnProperty(message => message.Text).Use(GetRandomValidText)
                .OnProperty(message => message.TelegramBotConfiguration).IgnoreIt()
                .OnType<DateTimeOffset>().Use(dates)
                .OnType<DateTimeOffset?>().Use(dates);

            return filler;
        }
    }
}

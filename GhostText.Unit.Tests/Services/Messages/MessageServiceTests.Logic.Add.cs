using FluentAssertions;
using Force.DeepCloner;
using GhostText.Models;
using Moq;

namespace GhostText.Unit.Tests.Services.Messages
{
    public partial class MessageServiceTests
    {
        [Fact]
        public async Task ShouldAddMessageAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            Message randomMessage = CreateRandomMessage(randomDateTimeOffset);
            Message inputMessage = randomMessage;
            Message persistedMessage = inputMessage;
            Message expectedMessage = persistedMessage.DeepClone();

            this.dateTimeRepositoryMock.Setup(repository =>
                repository.GetCurrentDateTime())
                    .Returns(randomDateTimeOffset);

            this.messageRepositoryMock.Setup(repository =>
                repository.InsertMessageAsync(inputMessage))
                    .ReturnsAsync(persistedMessage);

            // when
            Message actualMessage =
                await this.messageService.AddMessageAsync(inputMessage);

            // then
            actualMessage.Should().BeEquivalentTo(expectedMessage);

            this.dateTimeRepositoryMock.Verify(repository =>
                repository.GetCurrentDateTime(),
                    Times.Once);

            this.messageRepositoryMock.Verify(repository =>
                repository.InsertMessageAsync(inputMessage),
                    Times.Once);

            this.dateTimeRepositoryMock.VerifyNoOtherCalls();
            this.messageRepositoryMock.VerifyNoOtherCalls();
        }
    }
}

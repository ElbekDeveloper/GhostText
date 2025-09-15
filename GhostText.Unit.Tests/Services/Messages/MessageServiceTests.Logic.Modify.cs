using FluentAssertions;
using Force.DeepCloner;
using GhostText.Models;
using Moq;

namespace GhostText.Unit.Tests.Services.Messages
{
    public partial class MessageServiceTests
    {
        [Fact]
        public async Task ShouldModifyMessageAsync()
        {
            // given
            Message randomMessage = CreateRandomMessage(dates: GetRandomDateTimeOffset());
            Message inputMessage = randomMessage;
            Message retrievedMessage = inputMessage;
            Message expectedInputMessage = retrievedMessage;
            Message modifiedMessage = expectedInputMessage;
            Message expectedMessage = expectedInputMessage.DeepClone();

            this.messageRepositoryMock.Setup(repository =>
                repository.SelectMessageByIdAsync(inputMessage.Id))
                    .ReturnsAsync(retrievedMessage);

            this.messageRepositoryMock.Setup(repository =>
                repository.UpdateMessageAsync(expectedInputMessage))
                    .ReturnsAsync(modifiedMessage);

            // when
            Message actualMessage =
                await this.messageService.ModifyMessageAsync(inputMessage);

            // then
            actualMessage.Should().BeEquivalentTo(expectedMessage);

            this.messageRepositoryMock.Verify(repository =>
                repository.SelectMessageByIdAsync(inputMessage.Id),
                    Times.Once);

            this.messageRepositoryMock.Verify(repository =>
                repository.UpdateMessageAsync(expectedInputMessage),
                    Times.Once);

            this.messageRepositoryMock.VerifyNoOtherCalls();
            this.dateTimeRepositoryMock.VerifyNoOtherCalls();
        }
    }
}

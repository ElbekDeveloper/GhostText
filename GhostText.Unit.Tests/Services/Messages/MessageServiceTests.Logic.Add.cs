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
            Message randomMessage = CreateRandomMessage();
            Message inputMessage = randomMessage;
            Message persistedMessage = inputMessage;
            Message expectedMessage = persistedMessage.DeepClone();

            this.messageRepositoryMock.Setup(repository =>
                repository.InsertMessageAsync(inputMessage))
                    .ReturnsAsync(persistedMessage);

            // when
            Message actualMessage =
                await this.messageService.AddMessageAsync(inputMessage);

            // then
            actualMessage.Should().BeEquivalentTo(expectedMessage, option =>
                option.Excluding(message => message.CreateDate));

            this.messageRepositoryMock.Verify(repository =>
                repository.InsertMessageAsync(inputMessage),
                    Times.Never);

            this.messageRepositoryMock.VerifyNoOtherCalls();
        }
    }
}

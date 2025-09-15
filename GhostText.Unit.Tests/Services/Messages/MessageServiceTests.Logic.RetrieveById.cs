using FluentAssertions;
using Force.DeepCloner;
using GhostText.Models;
using Moq;

namespace GhostText.Unit.Tests.Services.Messages
{
    public partial class MessageServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveMessageByIdAsync()
        {
            // given
            Guid randomMessageId = GetRandomId();
            Guid inputMessageId = randomMessageId;
            
            Message randomMessage = CreateRandomMessage(
                dates: GetRandomDateTimeOffset());

            Message retrievedMessage = randomMessage;
            Message expectedMessage = retrievedMessage.DeepClone();

            this.messageRepositoryMock.Setup(repository =>
                repository.SelectMessageByIdAsync(inputMessageId))
                    .ReturnsAsync(retrievedMessage);

            // when
            Message actualMessage =
                await this.messageService.RetrieveMessageByIdAsync(inputMessageId);

            // then
            actualMessage.Should().BeEquivalentTo(expectedMessage);

            this.messageRepositoryMock.Verify(repository =>
                repository.SelectMessageByIdAsync(inputMessageId),
                    Times.Once);

            this.messageRepositoryMock.VerifyNoOtherCalls();
        }
    }
}

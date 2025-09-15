using FluentAssertions;
using GhostText.Models;
using Moq;

namespace GhostText.Unit.Tests.Services.Messages
{
    public partial class MessageServiceTests
    {
        [Fact]
        public async Task ShouldThrowArgumentExceptionOnRetrieveByIdIfMessageIdIsInvalidAsync()
        {
            // given
            Guid invalidMessageId = Guid.Empty;

            var expectedArgumentException =
                new ArgumentException("Message id is required.");

            // when
            ValueTask<Message> retrieveMessageByIdTask = 
                this.messageService.RetrieveMessageByIdAsync(invalidMessageId);

            ArgumentException actualArgumentException =
                await Assert.ThrowsAsync<ArgumentException>(
                    retrieveMessageByIdTask.AsTask);

            // then
            actualArgumentException.Message.Should()
                .BeEquivalentTo(expectedArgumentException.Message);

            this.messageRepositoryMock.Verify(repository =>
                repository.SelectMessageByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.messageRepositoryMock.VerifyNoOtherCalls();
            this.dateTimeRepositoryMock.VerifyNoOtherCalls();
        }
    }
}

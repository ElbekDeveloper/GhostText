using FluentAssertions;
using GhostText.Models;
using Moq;

namespace GhostText.Unit.Tests.Services.Messages
{
    public partial class MessageServiceTests
    {
        [Fact]
        public async Task ShouldThrowArgumentExceptionOnModifyIfInputIsNullAsync()
        {
            // given
            Message nullMessage = null;

            var expectedArgumentException =
                new ArgumentException("Message is null.");

            // when
            ValueTask<Message> modifyMessageTask =
                this.messageService.ModifyMessageAsync(nullMessage);

            ArgumentException actualArgumentException = 
                await Assert.ThrowsAsync<ArgumentException>(
                    modifyMessageTask.AsTask);

            // then
            actualArgumentException.Message.Should().BeEquivalentTo(
                expectedArgumentException.Message);

            this.messageRepositoryMock.Verify(repository =>
                repository.SelectMessageByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.messageRepositoryMock.Verify(repository =>
                repository.UpdateMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.messageRepositoryMock.VerifyNoOtherCalls();
            this.dateTimeRepositoryMock.VerifyNoOtherCalls();
        }
    }
}

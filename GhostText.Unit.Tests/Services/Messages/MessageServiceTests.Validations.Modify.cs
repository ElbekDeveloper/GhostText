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

        [Fact]
        public async Task ShouldThrowKeyNotFoundExceptionOnModifyIfMessageIsNotFoundAsync()
        {
            // given
            Message someMessage = CreateRandomMessage(GetRandomDateTimeOffset());
            Message nonExsistentMessage = null;

            var expectedKeyNotFoundException =
                new KeyNotFoundException(
                    $"Message is not found with given id: {someMessage.Id}.");

            this.messageRepositoryMock.Setup(repository =>
                repository.SelectMessageByIdAsync(someMessage.Id))
                    .ReturnsAsync(nonExsistentMessage);

            // when
            ValueTask<Message> modifyMessageTask =
                this.messageService.ModifyMessageAsync(someMessage);

            KeyNotFoundException actualKeyNotFoundException =
                await Assert.ThrowsAsync<KeyNotFoundException>(
                    modifyMessageTask.AsTask);

            // then
            actualKeyNotFoundException.Message.Should().BeEquivalentTo(
                expectedKeyNotFoundException.Message);

            this.messageRepositoryMock.Verify(repository =>
                repository.SelectMessageByIdAsync(It.IsAny<Guid>()),
                    Times.Once); 

            this.messageRepositoryMock.Verify(repository =>
                repository.UpdateMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.messageRepositoryMock.VerifyNoOtherCalls();
            this.dateTimeRepositoryMock.VerifyNoOtherCalls();
        }
    }
}

using FluentAssertions;
using GhostText.Models;
using Moq;

namespace GhostText.Unit.Tests.Services.Messages
{

    public partial class MessageServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowArgumentExceptionOnAddIfInputIsInvalidAsync(string invalidText)
        {
            // given
            var invalidMessage = new Message
            {
                Text = invalidText,
            };

            var expectedArgumentException = 
                new ArgumentException("Message text cannot be empty.");

            // when
            ValueTask<Message> addMessageTask = 
                this.messageService.AddMessageAsync(invalidMessage);

            ArgumentException actualArgumentException =
                await Assert.ThrowsAsync<ArgumentException>(
                    addMessageTask.AsTask);

            // then
            actualArgumentException.Message.Should()
                .BeEquivalentTo(expectedArgumentException.Message);

            this.dateTimeRepositoryMock.Verify(repository =>
                repository.GetCurrentDateTime(),
                    Times.Never);

            this.messageRepositoryMock.Verify(repository =>
                repository.InsertMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.dateTimeRepositoryMock.VerifyNoOtherCalls();
            this.messageRepositoryMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(CreateInvalidTexts))]
        public async Task ShouldThrowArgumentExceptionOnAddIfTextLengthIsInvalidAsync(
            string invalidText)
        {
            // given
            var invalidMessage = new Message
            {
                Text = invalidText,
            };

            string expectedExceptionMessage = 
                "Messge length should be between 15 and 120";

            // when
            ValueTask<Message> addMessageTask =
                this.messageService.AddMessageAsync(invalidMessage);

            ArgumentException actualArgumentException =
                await Assert.ThrowsAsync<ArgumentException>(
                    addMessageTask.AsTask);

            // then
            actualArgumentException.Message.Should().BeEquivalentTo(expectedExceptionMessage);

            this.dateTimeRepositoryMock.Verify(repository =>
                repository.GetCurrentDateTime(),
                    Times.Never);

            this.messageRepositoryMock.Verify(repository =>
                repository.InsertMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.dateTimeRepositoryMock.VerifyNoOtherCalls();
            this.messageRepositoryMock.VerifyNoOtherCalls();
        }
    }
}

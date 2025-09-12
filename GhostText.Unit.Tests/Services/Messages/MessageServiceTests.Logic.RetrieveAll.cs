using FluentAssertions;
using Force.DeepCloner;
using GhostText.Models;
using Moq;

namespace GhostText.Unit.Tests.Services.Messages
{
    public partial class MessageServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllMessages()
        {
            // given
            IQueryable<Message> randomMessages =
                CreateRandomMessages();

            IQueryable<Message> retrievedMessages = 
                randomMessages;

            IQueryable<Message> expectedMessages =
                retrievedMessages.DeepClone();

            this.messageRepositoryMock.Setup(repository =>
                repository.SelectAllMessages())
                    .Returns(retrievedMessages);

            // when
            IQueryable<Message> actualMessages = 
                this.messageService.RetrieveAllMessages();

            // then
            actualMessages.Should().BeEquivalentTo(expectedMessages);

            this.messageRepositoryMock.Verify(repository => 
                repository.SelectAllMessages(),
                    Times.Once);

            this.messageRepositoryMock.VerifyNoOtherCalls();
            this.dateTimeRepositoryMock.VerifyNoOtherCalls();
        }
    }
}

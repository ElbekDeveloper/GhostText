using FluentAssertions;
using GhostText.Models;
using Moq;

namespace GhostText.Unit.Tests.Services.TelegramUsers
{
    public partial class TelegramUserServiceTests
    {
        [Fact]
        public async Task ShouldThrowArgumentNullExceptionOnAddIfTelegramUserIsNullAsync()
        {
            // given
            TelegramUser nullTelegramUser = null;
            

            var expectedArgumentNullException =
                new ArgumentNullException( "TelegramUser cannot be null.");
            
            //when
            ValueTask<TelegramUser> addTelegramUserTask =
                this.telegramUserService.AddTelegramUserAsync(nullTelegramUser);
            
            ArgumentNullException actualArgumentNullException =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    addTelegramUserTask.AsTask);
            
            //then
            actualArgumentNullException.Message.Should()
                .BeEquivalentTo(expectedArgumentNullException.Message);
            
            this.telegramUserRepositoryMock.Verify(repository=>
                repository.InsertTelegramUserAsync(It.IsAny<TelegramUser>()),
                    Times.Never);
            
            this.telegramUserRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
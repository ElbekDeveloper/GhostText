using FluentAssertions;
using Force.DeepCloner;
using GhostText.Models;
using Moq;

namespace GhostText.Unit.Tests.Services.TelegramUsers
{
    public partial class TelegramUserServiceTests
    {
        [Fact]
        public async Task ShouldAddTelegramUserAsync()
        {
            //given
            TelegramUser randomTelegramUser = GetRandomTelegramUser();
            TelegramUser inputTelegramUser = randomTelegramUser;
            TelegramUser persistedTelegramUser = inputTelegramUser;
            TelegramUser expectedTelegramUser = persistedTelegramUser.DeepClone();
            
            
            this.telegramUserRepositoryMock.Setup(repository=> 
                repository.InsertTelegramUserAsync(inputTelegramUser))
                    .ReturnsAsync(persistedTelegramUser);

            //when
            TelegramUser actualTelegramUser =
                await this.telegramUserService.AddTelegramUserAsync(inputTelegramUser);
            
            //then
            actualTelegramUser.Should().BeEquivalentTo(expectedTelegramUser);
            
            this.telegramUserRepositoryMock.Verify(repository=>
                repository.InsertTelegramUserAsync(inputTelegramUser), 
                    Times.Once);
            
            this.telegramUserRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
using FluentAssertions;
using Force.DeepCloner;
using GhostText.Models;
using Moq;

namespace GhostText.Unit.Tests.Services.TelegramUsers;

public partial class TelegramUserServiceTests
{
    [Fact]
    public void ShouldAllRetrieveAllTelegramUsers()
    {
        //given
        IQueryable<TelegramUser> randomTelegramUsers = CreateRandomTelegramUsers();
        IQueryable<TelegramUser> retrievedTelegramUsers = randomTelegramUsers;
        IQueryable<TelegramUser> expectedTelegramUser = retrievedTelegramUsers.DeepClone();

        this.telegramUserRepositoryMock.Setup(repository => 
                repository.SelectAllTelegramUser())
                    .Returns(retrievedTelegramUsers);

        //when
        IQueryable<TelegramUser> actualTelegramUsers =
            this.telegramUserService.RetrieveAllTelegramUser();
        
        //then
        actualTelegramUsers.Should().BeEquivalentTo(expectedTelegramUser);
        
        this.telegramUserRepositoryMock.Verify(repository=>
            repository.SelectAllTelegramUser(), 
                Times.Once);
        
        this.telegramUserRepositoryMock.VerifyNoOtherCalls();
    }
}
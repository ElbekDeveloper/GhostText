using FluentAssertions;
using Force.DeepCloner;
using GhostText.Models.TelegramBotConfigurations;
using Moq;

namespace GhostText.Unit.Tests.Services.TelegramBotConfigurations
{
    public partial class TelegramBotConfigurationServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllTelegramBotConfigurations()
        {
            //given
            IQueryable<TelegramBotConfiguration> randomTelegramBotConfigurations =
                CreateRandomTelegramBotConfigurations();
            IQueryable<TelegramBotConfiguration> retrievedTelegramBotConfigurations =
                randomTelegramBotConfigurations;
            IQueryable<TelegramBotConfiguration> expectedTelegramBotConfigurations =
                retrievedTelegramBotConfigurations.DeepClone();
            
            this.telegramBotConfigurationRepositoryMock.Setup(repository=>
                repository.SelectAlltelegramBotConfigurations())
                .Returns(retrievedTelegramBotConfigurations);
            
            //when
            IQueryable<TelegramBotConfiguration> actualTelegramBotConfigurations =
                this.telegramBotConfigurationService.RetrieveAllTelegramBotConfigurations();
            
            //then
            actualTelegramBotConfigurations.Should().BeEquivalentTo(expectedTelegramBotConfigurations);
            
            this.telegramBotConfigurationRepositoryMock.Verify(repository=>
                repository.SelectAlltelegramBotConfigurations(),
                    Times.Once);
            
            this.telegramBotConfigurationRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
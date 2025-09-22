using FluentAssertions;
using Force.DeepCloner;
using GhostText.Models.TelegramBotConfigurations;
using Moq;

namespace GhostText.Unit.Tests.Services.TelegramBotConfigurations
{
    public partial class TelegramBotConfigurationServiceTests
    {
        [Fact]
        public async Task ShouldAddTelegramBotConfigurationAsync()
        {
            // given
            TelegramBotConfiguration randomTelegramBotConfiguration = 
                CreateRandomTelegramBotConfiguration();
            
            TelegramBotConfiguration inputTelegramBotConfiguration = 
                randomTelegramBotConfiguration;
            
            TelegramBotConfiguration persistedTelegramBotConfiguration = 
                inputTelegramBotConfiguration;
            
            TelegramBotConfiguration expectedTelegramBotConfiguration=
                persistedTelegramBotConfiguration.DeepClone();
            
            this.telegramBotConfigurationRepositoryMock.Setup(repository=>
                repository.InsertChannelAsync(inputTelegramBotConfiguration))
                    .ReturnsAsync(persistedTelegramBotConfiguration);
            
            // when
            TelegramBotConfiguration actualTelegramBotConfiguration =
                await this.telegramBotConfigurationService
                    .AddTelegramBotConfigurationAsync(inputTelegramBotConfiguration);
            
            // then
            actualTelegramBotConfiguration.Should().BeEquivalentTo(expectedTelegramBotConfiguration);
            
            this.telegramBotConfigurationRepositoryMock.Verify(repository=>
                repository.InsertChannelAsync(inputTelegramBotConfiguration),
                    Times.Once);
            
            this.telegramBotConfigurationRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
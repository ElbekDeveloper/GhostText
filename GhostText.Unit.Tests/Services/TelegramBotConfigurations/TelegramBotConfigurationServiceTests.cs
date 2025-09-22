using GhostText.Models.TelegramBotConfigurations;
using GhostText.Repositories.TelegramBotConfigurations;
using GhostText.Services.TelegramBotConfigurations;
using Moq;
using Tynamix.ObjectFiller;

namespace GhostText.Unit.Tests.Services.TelegramBotConfigurations
{
    public partial class TelegramBotConfigurationServiceTests
    {
        private readonly TelegramBotConfigurationService telegramBotConfigurationService;
        private readonly Mock<ITelegramBotConfigurationRepository> telegramBotConfigurationRepositoryMock;

        public TelegramBotConfigurationServiceTests()
        {
            this.telegramBotConfigurationRepositoryMock = 
                new Mock<ITelegramBotConfigurationRepository>();
            
            this.telegramBotConfigurationService= 
                new TelegramBotConfigurationService(
                    this.telegramBotConfigurationRepositoryMock.Object);
        }

        private static long GetRandomValidChannelId()=>
            new LongRange(min:2,max:9).GetValue();
        
        private static string RandomValidToken()=>
            new MnemonicString().GetValue();

        private static DateTimeOffset GetRandomDateTimeOffset()=>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();
        
        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static IQueryable<TelegramBotConfiguration> CreateRandomTelegramBotConfigurations()
        {
            int randomCount=GetRandomNumber();
            
            return CreateTelegramBotConfigurationFiller().Create(randomCount).AsQueryable();
        }

        private static TelegramBotConfiguration CreateRandomTelegramBotConfiguration()=>
            CreateTelegramBotConfigurationFiller().Create();
        
        private static Filler<TelegramBotConfiguration> CreateTelegramBotConfigurationFiller()
        {
            var filler = new Filler<TelegramBotConfiguration>();

            filler.Setup()
                .OnType<DateTimeOffset>()
                    .Use(GetRandomDateTimeOffset());
            
            return filler;
        }
    }
}
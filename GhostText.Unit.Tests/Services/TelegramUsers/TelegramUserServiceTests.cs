using GhostText.Models;
using GhostText.Repositories;
using GhostText.Services;
using Moq;
using Tynamix.ObjectFiller;

namespace GhostText.Unit.Tests.Services.TelegramUsers
{
    public partial class TelegramUserServiceTests
    {
        private readonly TelegramUserService telegramUserService;
        private readonly Mock<ITelegramUserRepository> telegramUserRepositoryMock;

        public TelegramUserServiceTests()
        {
           this.telegramUserRepositoryMock = new Mock<ITelegramUserRepository>();
           this.telegramUserService = new TelegramUserService(this.telegramUserRepositoryMock.Object);
        }

        private static string GetRandomValidUsername()
        {
            return new MnemonicString(
                wordCount: 1,
                wordMinLength: 1,
                10).GetValue();
        }

        private static string GetRandomValidFullName()
        {
            return new MnemonicString(
                wordCount:2,
                wordMinLength: 2,
                wordMaxLength:20).GetValue();
        }

        private static long GetRandomValidTelegramId()
        {
            Random randomNumber = new Random();
            long randomTelegramId = randomNumber.Next(100000000, int.MaxValue);
            return randomTelegramId;
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static IQueryable<TelegramUser> CreateRandomTelegramUsers()
        {
            int randomCount = GetRandomNumber();
            
            return CreateTelegramUserFiller().Create(randomCount).AsQueryable();
        }
        
        private static TelegramUser GetRandomTelegramUser() =>
            CreateTelegramUserFiller().Create();

        private static Filler<TelegramUser> CreateTelegramUserFiller()
        {
            var filler = new Filler<TelegramUser>();
            
            filler.Setup()
                .OnProperty(telegramUser => telegramUser.UserName).Use(GetRandomValidUsername)
                .OnProperty(telegramUser => telegramUser.FullName).Use(GetRandomValidFullName)
                .OnProperty(telegramUser => telegramUser.TelegramId).Use(GetRandomValidTelegramId);
            
            return filler;
        }
    }
}
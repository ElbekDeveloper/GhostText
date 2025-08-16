using System.Threading.Tasks;
using GhostText.Data;
using GhostText.Models.TelegramBotConfiguration;
using Microsoft.EntityFrameworkCore;

namespace GhostText.Repositories.TelegramBotConfigurations
{
    public class TelegramBotConfigurationRepository : ITelegramBotConfigurationRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public TelegramBotConfigurationRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async ValueTask<TelegramBotConfiguration> InsertAsync(TelegramBotConfiguration configuration)
        {
            this.applicationDbContext.Entry(configuration).State = EntityState.Added;
            await this.applicationDbContext.SaveChangesAsync();

            return configuration;
        }

        public async ValueTask<TelegramBotConfiguration> SelectByChannelIdAsync(long channelId)
        {
            return await this.applicationDbContext.TelegramBotConfigurations
                    .FirstOrDefaultAsync(config => config.ChannelId == channelId);
        }

        public async ValueTask<TelegramBotConfiguration> SelectByTokenAsync(string token)
        {
            var config = await this.applicationDbContext.TelegramBotConfigurations
        .FirstOrDefaultAsync(c => c.Token == token);

            return config;
        }
    }
}

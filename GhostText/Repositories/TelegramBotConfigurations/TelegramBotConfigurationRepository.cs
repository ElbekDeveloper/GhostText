using System.Linq;
using System.Threading.Tasks;
using GhostText.Data;
using GhostText.Models.TelegramBotConfigurations;
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

        public async ValueTask<TelegramBotConfiguration>
            InsertChannelAsync(TelegramBotConfiguration configuration)
        {
            this.applicationDbContext.Entry(configuration).State = EntityState.Added;
            await this.applicationDbContext.SaveChangesAsync();

            return configuration;
        }


        public   IQueryable<TelegramBotConfiguration> SelectAlltelegramBotConfigurations()
        {
            return this.applicationDbContext.TelegramBotConfigurations.AsQueryable();
        }
    }
}

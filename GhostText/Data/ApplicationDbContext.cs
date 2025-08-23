using GhostText.Models;
using GhostText.Models.TelegramBotConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GhostText.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ApplicationDbContext(IConfiguration configuration) =>
            this.configuration = configuration;

        public DbSet<Message> Messages { get; set; }
        public DbSet<TelegramUser> TelegramUsers { get; set; }
        public DbSet<TelegramBotConfiguration> TelegramBotConfigurations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<TelegramUser>()
                .HasIndex(user => user.TelegramId)
                .IsUnique(true);

            modelBuilder.Entity<TelegramBotConfiguration>()
                .HasIndex(telegramBotConfiguration => telegramBotConfiguration.ChannelId)
                .IsUnique(true);

            modelBuilder.Entity<TelegramBotConfiguration>()
                .HasIndex(telegramBotConfiguration => telegramBotConfiguration.Token)
                .IsUnique(true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = 
                this.configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}

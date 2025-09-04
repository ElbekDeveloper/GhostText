using GhostText.Models;
using GhostText.Models.TelegramBotConfigurations;
using GhostText.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace GhostText.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            // this.Database.Migrate();
        }


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

            EntityTypeBuilder<Message> messageEntity = modelBuilder.Entity<Message>();

            messageEntity.Property(m => m.IsSent)
                         .HasDefaultValue(false);

            messageEntity.HasIndex(m => new { m.IsSent, m.CreateDate })
                         .HasDatabaseName("ix_messages_issent_createdate");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
                this.configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}

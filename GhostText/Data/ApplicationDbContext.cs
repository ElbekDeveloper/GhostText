using GhostText.Models;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = 
                this.configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}

using GhostText.Models;
using Microsoft.EntityFrameworkCore;

namespace GhostText.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)

        { }

        public DbSet<Message> Students { get; set; }
    }
}

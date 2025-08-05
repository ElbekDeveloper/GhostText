using LearnEfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnEfCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 

        { }

        public DbSet<Student>   Students { get; set; }
    }
}

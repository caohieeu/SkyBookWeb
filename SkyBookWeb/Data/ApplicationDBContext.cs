using Microsoft.EntityFrameworkCore;
using SkyBookWeb.Models;

namespace SkyBookWeb.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", Order = 1 },
                new Category { Id = 2, Name = "Drama", Order = 2 },
                new Category { Id = 3, Name = "Romance", Order = 3 }
            );
        }
    }
}

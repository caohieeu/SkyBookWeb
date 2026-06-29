using Microsoft.EntityFrameworkCore;
using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Infrastructure.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}

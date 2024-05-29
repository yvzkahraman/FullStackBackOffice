using BackOffice.Data;
using BackOffice.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackOffice
{
    public class ReactDbContext : DbContext
    {
        public ReactDbContext(DbContextOptions<ReactDbContext> options) :base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Product> Products{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
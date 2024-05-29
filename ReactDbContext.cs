using BackOffice.Data;
using BackOffice.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackOffice
{
    public class ReactDbContext : DbContext
    {
        // public ReactDbContext(DbContextOptions<ReactDbContext> options) :base(options)
        // {
        // scaffold  
        // Migration => Gönderiyor Database 
        // }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Product> Products{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.; database=ReactBootcampDb2; user id=sa; password=<YourStrong@Passw0rd>;TrustServerCertificate=true;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
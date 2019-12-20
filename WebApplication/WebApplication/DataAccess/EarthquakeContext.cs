using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.DataAccess
{
    public class EarthquakeContext : DbContext
    {
        public EarthquakeContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Username = "Alex",
                    Password = "Alex123"
                });
        }
    }
}

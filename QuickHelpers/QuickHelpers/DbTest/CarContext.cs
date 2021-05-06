using Microsoft.EntityFrameworkCore;

namespace QuickHelpers.DbTest
{
    public sealed class CarContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
     
        public CarContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./DbTest/CarDb.db");
        }
    }
}
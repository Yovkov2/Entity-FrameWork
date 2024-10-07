using Microsoft.EntityFrameworkCore;
using MigrationsDemo.Models;

namespace MigrationsDemo.Data
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(Environment.GetEnvironmentVariable("DATABASE_PROVIDER") == "PostgreeSQL")
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=school_db;User=postgres;Password=postgres")
                    .UseSnakeCaseNamingConvention();
            }
            else
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=School;Trusted_Connection=True;");
            }
        }
    }
}

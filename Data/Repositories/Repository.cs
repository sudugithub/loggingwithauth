using Data.Domain;
using Microsoft.EntityFrameworkCore;
using Service.Utils;

namespace Data.Repositories
{
    public class Repository(DbContextOptions<Repository> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<LogEvent> LogEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Test",
                Email = "admin@gmail.com",
                Password = PasswordHasher.Hash("Password@123")
            });
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Catalyst.Api.Main.Models
{
    public class ProgramContext : DbContext
    {
        public ProgramContext(DbContextOptions<ProgramContext> options) : base(options)
        {
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasData(new Project
            {
                Id = 1,
                Name = "Your first project",
                Description = "This is your first project."
            });

            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Name = "English", Dialect = "United States", ProjectId = 1 },
                new Language { Id = 2, Name = "Spanish", Dialect = "Spain", ProjectId = 1 }
            );

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Admin",
                Email = "admin@localhost",
                Password = new PasswordHasher<User>().HashPassword(null, "admin")
            });
        }
    }
}
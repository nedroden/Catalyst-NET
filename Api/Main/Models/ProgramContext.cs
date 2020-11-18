using Microsoft.EntityFrameworkCore;

namespace Catalyst.Api.Main.Models
{
    public class ProgramContext : DbContext
    {
        public ProgramContext(DbContextOptions<ProgramContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Language> Languages { get; set; }
    }
}
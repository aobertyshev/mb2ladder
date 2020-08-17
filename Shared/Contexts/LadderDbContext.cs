using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Shared.Contexts
{
    public sealed class LadderDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        // public DbSet<Queue> Queues { get; set; }
        public DbSet<Match> Matches { get; set; }
        // public DbSet<Team> Teams { get; set; }

        public LadderDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(System.Environment.GetEnvironmentVariable("MBII_LADDER_DB_CONNECTION_STRING"));
    }
}
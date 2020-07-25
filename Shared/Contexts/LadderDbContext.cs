using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Shared.Contexts
{
    public sealed class LadderDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }

        public LadderDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=postgres;CommandTimeout=0;Timeout=0;");
    }
}
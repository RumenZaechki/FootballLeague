using FootballLeague.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Domain
{
    public class FootballDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public FootballDbContext(DbContextOptions<FootballDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatchHistory)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatchHistory)
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team);

            modelBuilder.Entity<Team>()
                .Navigation(t => t.AwayMatchHistory)
                .HasField("awayMatches");

            modelBuilder.Entity<Team>()
                .Navigation(t => t.HomeMatchHistory)
                .HasField("homeMatches");
        }
    }
}

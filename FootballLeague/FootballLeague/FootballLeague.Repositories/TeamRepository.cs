using FootballLeague.Domain;
using FootballLeague.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly FootballDbContext data;
        public TeamRepository(FootballDbContext data)
        {
            this.data = data;
        }

        public async Task<Team> CreateTeamAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            var team = new Team(name);
            await this.data.Teams.AddAsync(team);
            await this.data.SaveChangesAsync();
            return team;
        }

        public async Task<Team> ReadOneAsync(int id)
        {
            var team = await this.data.Teams.FindAsync(id);
            return team;
        }

        public async Task<List<Team>> ReadAllAsync()
        {
            var teams = await this.data.Teams.ToListAsync();
            return teams;
        }

        public async Task<Team> UpdateAsync(int id, string name, int wins, int draws, int losses, int rank)
        {
            if (!IsValid(name, wins, draws, losses, rank) || !this.data.Teams.Any(t => t.Id == id))
            {
                return null;
            }
            var team = await this.data.Teams.FindAsync(id);
            team.Name = name;
            team.Wins = wins;
            team.Draws = draws;
            team.Losses = losses;
            team.Rank = rank;
            await this.data.SaveChangesAsync();
            return team;
        }

        public async Task<Team> DeleteAsync(int id)
        {
            var team = await this.data.Teams
                .Include(t => t.Players)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (team == null)
            {
                return null;
            }
            
            this.data.Teams.Remove(team);
            await this.data.SaveChangesAsync();
            return team;
        }

        private bool IsValid(string name, int wins, int draws, int losses, int rank)
        {
            if (string.IsNullOrEmpty(name) || wins < 0 || draws <0 || losses < 0 || rank == null)
            {
                return false;
            }
            return true;
        }
    }
}

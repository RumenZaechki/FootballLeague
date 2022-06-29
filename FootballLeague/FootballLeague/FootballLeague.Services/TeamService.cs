using FootballLeague.Repositories;
using FootballLeague.Services.Models.Teams;

namespace FootballLeague.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository teamRepository;
        public TeamService(ITeamRepository teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        public async Task<TeamServiceModel> CreateAsync(string name)
        {
            var result = await this.teamRepository.CreateTeamAsync(name);
            var team = new TeamServiceModel//this is getting a bit repetitive as well, maybe I should use a mapper, but I'll see what I can do later
            {
                Name = result.Name,
                Wins = result.Wins,
                Draws = result.Draws,
                Losses = result.Losses,
                Rank = result.Rank,
                Points = result.Points
            };
            return team;
        }

        public async Task<TeamServiceModel> ReadOneAsync(int id)
        {
            var result = await this.teamRepository.ReadOneAsync(id);
            if (result == null)
            {
                return null;
            }
            var team = new TeamServiceModel
            {
                Name = result.Name,
                Wins = result.Wins,
                Draws = result.Draws,
                Losses = result.Losses,
                Rank = result.Rank,
                Points = result.Points
            };
            return team;
        }

        public async Task<List<TeamServiceModel>> ReadAllAsync()
        {
            var result = await this.teamRepository.ReadAllAsync();
            return result
                .Select(t => new TeamServiceModel
                {
                    Name = t.Name,
                    Wins = t.Wins,
                    Draws = t.Draws,
                    Losses = t.Losses,
                    Rank = t.Rank,
                    Points = t.Points
                })
                .ToList();
        }

        public async Task<TeamServiceModel> UpdateAssync(int id, string name, int wins, int draws, int losses, int rank)
        {
            var result = await this.teamRepository.UpdateAsync(id, name, wins, draws, losses, rank);
            if (result == null)
            {
                return null;
            }
            var team = new TeamServiceModel
            {
                Name = result.Name,
                Wins = result.Wins,
                Draws = result.Draws,
                Losses = result.Losses,
                Rank = result.Rank,
                Points = result.Points
            };
            return team;
        }

        public async Task<TeamServiceModel> DeleteAsync(int id)
        {
            var result = await this.teamRepository.DeleteAsync(id);
            if (result == null)
            {
                return null;
            }
            var team = new TeamServiceModel
            {
                Name = result.Name,
                Wins = result.Wins,
                Draws = result.Draws,
                Losses = result.Losses,
                Rank = result.Rank,
                Points = result.Points
            };
            return team;
        }
    }
}

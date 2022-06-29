using FootballLeague.Repositories;
using FootballLeague.Services.Models;

namespace FootballLeague.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository playerRepository;
        public PlayerService(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public async Task<PlayerServiceModel> CreatePlayerAsync(string firstName, string lastName, string Position, int Skill, DateTime birthDate, int teamId)
        {
            var result = await this.playerRepository.CreatePlayerAsync(firstName, lastName, Position, Skill, birthDate, teamId);
            if (result == null)
            {
                return null;
            }
            var player = new PlayerServiceModel
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Position = result.Position.ToString(),
                Skill = result.Skill,
                BirthDate = result.BirthDate,
                TeamName = result.Team != null ? result.Team.Name : "none"
            };
            return player;
        }

        public async Task<PlayerServiceModel> ReadOneAsync(int id)
        {
            var result = await this.playerRepository.ReadOneAsync(id);
            if (result == null)
            {
                return null;
            }
            var player = new PlayerServiceModel
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Position = result.Position.ToString(),
                Skill = result.Skill,
                BirthDate = result.BirthDate,
                TeamName = result.Team != null ? result.Team.Name : "none"
            };
            return player;
        }

        public async Task<List<PlayerServiceModel>> ReadAllNotInTeamAsync()
        {
            var result = await this.playerRepository.ReadAllNotInTeamAsync();
            return result
                .Select(p => new PlayerServiceModel
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Position = p.Position.ToString(),
                    Skill = p.Skill,
                    BirthDate = p.BirthDate,
                    TeamName = "none"
                })
                .ToList();
        }

        public async Task<PlayerServiceModel> UpdateAsync(int id, string firstName, string lastName, string Position, int Skill, DateTime birthDate, int teamId)
        {
            var result = await this.playerRepository.UpdateAsync(id, firstName, lastName, Position, Skill, birthDate, teamId);
            if (playerRepository == null)//I know this check gets a bit repetitive, I probably should put it in a separate method, but I'll probably do that at a later time
            {
                return null;
            }
            var player = new PlayerServiceModel
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Position = result.Position.ToString(),
                Skill = result.Skill,
                BirthDate = result.BirthDate,
                TeamName = result.Team != null ? result.Team.Name : "none"
            };
            return player;
        }

        public async Task<PlayerServiceModel> DeleteAsync(int id)
        {
            var result = await this.playerRepository.DeleteAsync(id);
            if (result == null)
            {
                return null;
            }
            var player = new PlayerServiceModel
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Position = result.Position.ToString(),
                Skill = result.Skill,
                BirthDate = result.BirthDate,
                TeamName = result.Team != null ? result.Team.Name : "none"
            };
            return player;
        }
    }
}

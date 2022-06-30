using FootballLeague.Domain;
using FootballLeague.Domain.Models;
using FootballLeague.Domain.Models.Utility;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly FootballDbContext data;
        public PlayerRepository(FootballDbContext data)
        {
            this.data = data;
        }

        public async Task<Player> CreatePlayerAsync(string firstName, string lastName, string position, int skill, DateTime birthDate, int teamId)
        {
            Enum.TryParse(typeof(PositionType), position, true, out object result);
            if (!IsValid(firstName, lastName, position, skill, birthDate))
            {
                return null;
            }
            Player player = new Player(firstName, lastName, skill, (PositionType)result, birthDate);
            if (TeamExists(teamId))
            {
                player.Team = await this.data.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
            }
            await this.data.Players.AddAsync(player);
            await this.data.SaveChangesAsync();
            return player;
        }

        public async Task<Player> ReadOneAsync(int id)
        {
            var player = await this.data.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == id);
            return player;
        }

        public async Task<List<Player>> ReadAllNotInTeamAsync()
        {
            return await this.data.Players
                 .Where(p => p.Team == null)
                 .ToListAsync();
        }

        public async Task<Player> UpdateAsync(int id, string firstName, string lastName, string position, int skill, DateTime birthDate, int teamId)
        {
            var player = await this.data.Players.FindAsync(id);
            if (player == null || !IsValid(firstName, lastName, position, skill, birthDate))
            {
                return null;
            }
            Enum.TryParse(typeof(PositionType), position, true, out object result);
            player.FirstName = firstName;
            player.LastName = lastName;
            player.Position = (PositionType)result;
            player.Skill = skill;
            player.BirthDate = birthDate;
            if (TeamExists(teamId))
            {
                var team = await this.data.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
                player.Team = team;
                team.Players.Add(player);
            }
            this.data.SaveChangesAsync();
            return player;
        }

        public async Task<Player> DeleteAsync(int id)
        {
            var player = await this.data.Players.FindAsync(id);
            if (player != null)
            {
                this.data.Players.Remove(player);
                await this.data.SaveChangesAsync();
            }
            return player;
        }

        private bool TeamExists(int id)
        {
            return this.data.Teams.Any(t => t.Id == id);
        }

        private bool IsValid(string firstName, string lastName, string position, int skill, DateTime birthDate)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || skill <= 0 || birthDate == DateTime.MinValue
                || !Enum.TryParse(typeof(PositionType), position, true, out object result))
            {
                return false;
            }
            return true;
        }
    }
}

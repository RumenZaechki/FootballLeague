using FootballLeague.Domain.Models;

namespace FootballLeague.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> CreatePlayerAsync(string firstName, string lastName, string position, int skill, DateTime birthDate, int teamId);
        Task<Player> ReadOneAsync(int id);
        Task<List<Player>> ReadAllNotInTeamAsync();
        Task<Player> UpdateAsync(int id, string firstName, string lastName, string position, int skill, DateTime birthDate, int teamId);
        Task<Player> DeleteAsync(int id);
    }
}

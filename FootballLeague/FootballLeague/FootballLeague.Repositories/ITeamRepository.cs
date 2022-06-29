using FootballLeague.Domain.Models;

namespace FootballLeague.Repositories
{
    public interface ITeamRepository
    {
        Task<Team> CreateTeamAsync(string name);
        Task<Team> ReadOneAsync(int id);
        Task<List<Team>> ReadAllAsync();
        Task<Team> UpdateAsync(int id, string name, int wins, int draws, int losses, int rank);
        Task<Team> DeleteAsync(int id);
    }
}

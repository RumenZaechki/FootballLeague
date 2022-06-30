using FootballLeague.Domain.Models;

namespace FootballLeague.Repositories
{
    public interface IMatchRepository
    {
        Task<Match> CreateAsync(int homeTeamId, int awayTeamId, string stadium);
        Task<List<Match>> ReadAllPlayedAsync();
        Task<Match> ReadOneAsync(int id);
        Task<string> GetTeamNameFromMatchAsync(int id);
        Task<Match> PlayAsync(int id);
        Task<Match> DeleteAsync(int id);
    }
}

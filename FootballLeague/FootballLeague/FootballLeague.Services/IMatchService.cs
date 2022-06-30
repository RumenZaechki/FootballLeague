using FootballLeague.Services.Models.Matches;

namespace FootballLeague.Services
{
    public interface IMatchService
    {
        Task<MatchServiceModel> CreateAsync(int homeTeamId, int awayTeamId, string stadium);
        Task<List<MatchServiceModel>> ReadAllPlayedAsync();
        Task<MatchServiceModel> ReadOneAsync(int id);
        Task<MatchServiceModel> PlayAsync(int id);
        Task<MatchServiceModel> DeleteAsync(int id);
    }
}

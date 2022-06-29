using FootballLeague.Services.Models.Teams;

namespace FootballLeague.Services
{
    public interface ITeamService
    {
        Task<TeamServiceModel> CreateAsync(string name);
        Task<TeamServiceModel> ReadOneAsync(int id);
        Task<List<TeamServiceModel>> ReadAllAsync();
        Task<TeamServiceModel> UpdateAssync(int id, string name, int wins, int draws, int losses, int rank);
        Task<TeamServiceModel> DeleteAsync(int id);
    }
}

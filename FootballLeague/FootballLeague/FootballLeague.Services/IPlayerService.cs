using FootballLeague.Services.Models;

namespace FootballLeague.Services
{
    public interface IPlayerService
    {
        Task<PlayerServiceModel> CreatePlayerAsync(string firstName, string lastName, string Position, int Skill, DateTime birthDate, int teamId);
        Task<PlayerServiceModel> ReadOneAsync(int id);
        Task<List<PlayerServiceModel>> ReadAllNotInTeamAsync();
        Task<PlayerServiceModel> UpdateAsync(int id, string firstName, string lastName, string Position, int Skill, DateTime birthDate, int teamId);
        Task<PlayerServiceModel> DeleteAsync(int id);
    }
}

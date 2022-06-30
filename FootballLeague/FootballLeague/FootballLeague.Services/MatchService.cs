using FootballLeague.Repositories;
using FootballLeague.Services.Models.Matches;

namespace FootballLeague.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository matchRepository;
        public MatchService(IMatchRepository matchRepository)
        {
            this.matchRepository = matchRepository;
        }

        public async Task<MatchServiceModel> CreateAsync(int homeTeamId, int awayTeamId, string stadium)
        {
            var result = await this.matchRepository.CreateAsync(homeTeamId, awayTeamId, stadium);
            if (result == null)
            {
                return null;
            }
            return new MatchServiceModel
            {
                PlayedOn = result.PlayedOn,
                HomeTeamId = result.HomeTeamId,
                HomeTeamName = result.HomeTeam.Name,
                AwayTeamId = result.AwayTeamId,
                AwayTeamName = result.AwayTeam.Name,
                HomeScore = result.HomeScore,
                AwayScore = result.AwayScore,
                Stadium = result.Stadium
            };
        }

        public async Task<MatchServiceModel> ReadOneAsync(int id)
        {
            var result = await this.matchRepository.ReadOneAsync(id);
            if (result == null)
            {
                return null;
            }
            return new MatchServiceModel
            {
                PlayedOn = result.PlayedOn,
                HomeTeamId = result.HomeTeamId,
                HomeTeamName = result.HomeTeam.Name,
                AwayTeamId = result.AwayTeamId,
                AwayTeamName = result.AwayTeam.Name,
                HomeScore = result.HomeScore,
                AwayScore = result.AwayScore,
                Stadium = result.Stadium
            };
        }

        public async Task<List<MatchServiceModel>> ReadAllPlayedAsync()
        {
            var result = await this.matchRepository.ReadAllPlayedAsync();
            return result
                .Select(m => new MatchServiceModel
                {
                    PlayedOn = m.PlayedOn,
                    HomeTeamId = m.HomeTeamId,
                    HomeTeamName = m.HomeTeam.Name,
                    AwayTeamId = m.AwayTeamId,
                    AwayTeamName = m.AwayTeam.Name,
                    HomeScore = m.HomeScore,
                    AwayScore = m.AwayScore,
                    Stadium = m.Stadium
                })
                .ToList();
        }

        public async Task<MatchServiceModel> PlayAsync(int id)
        {
            var result = await this.matchRepository.PlayAsync(id);
            if (result == null)
            {
                return null;
            }
            return new MatchServiceModel
            {
                PlayedOn = result.PlayedOn,
                HomeTeamId = result.HomeTeamId,
                HomeTeamName = result.HomeTeam.Name,
                AwayTeamId = result.AwayTeamId,
                AwayTeamName = result.AwayTeam.Name,
                HomeScore = result.HomeScore,
                AwayScore = result.AwayScore,
                Stadium = result.Stadium
            };
        }

        public async Task<MatchServiceModel> DeleteAsync(int id)
        {
            var result = await this.matchRepository.DeleteAsync(id);
            if (result == null)
            {
                return null;
            }
            return new MatchServiceModel
            {
                PlayedOn = result.PlayedOn,
                HomeTeamId = result.HomeTeamId,
                HomeTeamName = result.HomeTeam.Name,
                AwayTeamId = result.AwayTeamId,
                AwayTeamName = result.AwayTeam.Name,
                HomeScore = result.HomeScore,
                AwayScore = result.AwayScore,
                Stadium = result.Stadium
            };
        }
    }
}

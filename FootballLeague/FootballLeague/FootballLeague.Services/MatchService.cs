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
                HomeTeamName = await this.matchRepository.GetTeamNameFromMatchAsync(result.HomeTeamId),
                AwayTeamId = result.AwayTeamId,
                AwayTeamName = await this.matchRepository.GetTeamNameFromMatchAsync(result.AwayTeamId),
                HomeScore = result.HomeScore,
                AwayScore = result.AwayScore,
                Stadium = result.Stadium
            };
        }

        public async Task<List<MatchServiceModel>> ReadAllPlayedAsync()
        {
            var result = await this.matchRepository.ReadAllPlayedAsync();
            var models = new List<MatchServiceModel>();
            foreach (var item in result)
            {
                var model = new MatchServiceModel
                {
                    PlayedOn = item.PlayedOn,
                    HomeTeamId = item.HomeTeamId,
                    HomeTeamName = await this.matchRepository.GetTeamNameFromMatchAsync(item.HomeTeamId),
                    AwayTeamId = item.AwayTeamId,
                    AwayTeamName = await this.matchRepository.GetTeamNameFromMatchAsync(item.AwayTeamId),
                    HomeScore = item.HomeScore,
                    AwayScore = item.AwayScore,
                    Stadium = item.Stadium
                };
                models.Add(model);
            }
            return models;
            //this doesn't seem to work, I guess I'll have to do it the old-fashioned way
            //return result
            //    .Select(async m => new MatchServiceModel
            //    {
            //        PlayedOn = m.PlayedOn,
            //        HomeTeamId = m.HomeTeamId,
            //        HomeTeamName = await this.matchRepository.GetTeamNameFromMatchAsync(m.HomeTeamId),
            //        AwayTeamId = m.AwayTeamId,
            //        AwayTeamName = await this.matchRepository.GetTeamNameFromMatchAsync(m.AwayTeamId),
            //        HomeScore = m.HomeScore,
            //        AwayScore = m.AwayScore,
            //        Stadium = m.Stadium
            //    })
            //    .ToList();
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
                HomeTeamName = await this.matchRepository.GetTeamNameFromMatchAsync(result.HomeTeamId),
                AwayTeamId = result.AwayTeamId,
                AwayTeamName = await this.matchRepository.GetTeamNameFromMatchAsync(result.AwayTeamId),
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
                HomeTeamName = await this.matchRepository.GetTeamNameFromMatchAsync(result.HomeTeamId),
                AwayTeamId = result.AwayTeamId,
                AwayTeamName = await this.matchRepository.GetTeamNameFromMatchAsync(result.AwayTeamId),
                HomeScore = result.HomeScore,
                AwayScore = result.AwayScore,
                Stadium = result.Stadium
            };
        }
    }
}

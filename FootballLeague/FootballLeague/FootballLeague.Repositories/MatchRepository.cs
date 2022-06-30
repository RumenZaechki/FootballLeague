using FootballLeague.Domain;
using FootballLeague.Domain.Models;
using FootballLeague.Domain.Models.Utility;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly FootballDbContext data;
        public MatchRepository(FootballDbContext data)
        {
            this.data = data;
        }

        private IDictionary<ResultType, Action<Team>> ResultActions => new Dictionary<ResultType, Action<Team>>()
        {
            [ResultType.Win] = (team) =>
            {
                team.Points += 3;
                team.Wins++;
            },
            [ResultType.Draw] = (team) =>
            {
                team.Points += 1;
                team.Draws++;
            },
            [ResultType.Lose] = (team) =>
            {
                team.Losses++;
            }
        };

        public async Task<Match> CreateAsync(int homeTeamId, int awayTeamId, string stadium)
        {
            Team homeTeam = await this.data.Teams.FindAsync(homeTeamId);
            Team awayTeam = await this.data.Teams.FindAsync(awayTeamId);
            if (string.IsNullOrEmpty(stadium) || homeTeam == null || awayTeam == null)
            {
                return null;
            }
            Match match = new Match(homeTeam, awayTeam, stadium);
            await this.data.Matches.AddAsync(match);
            await this.data.SaveChangesAsync();
            return match;
        }

        public async Task<Match> ReadOneAsync(int id)
        {
            var match = await this.data.Matches.FindAsync(id);
            if (match == null)
            {
                return null;
            }
            return match;
        }

        public async Task<string> GetTeamNameFromMatchAsync(int id)
        {
            Team team = await this.data.Teams.FindAsync(id);
            return team.Name;//I'm not sure whether this methoud should be here or in the TeamRepository class
        }

        public async Task<List<Match>> ReadAllPlayedAsync()
        {
            return await this.data.Matches
                .Where(m => m.PlayedOn != null)
                .ToListAsync();
        }

        public async Task<Match> PlayAsync(int id)
        {
            //I guess this will be the update method for the match - there's no point in changing the homeTeam name after the match is played, for instance
            var match = await this.data.Matches.FindAsync(id);
            if (match == null)
            {
                return null;
            }

            var homeTeam = await this.data.Teams.FindAsync(match.HomeTeamId);
            var awayTeam = await this.data.Teams.FindAsync(match.AwayTeamId);

            var homeTeamSkill = this.data.Players.Where(p => p.Team.Id == match.HomeTeamId).Sum(p => p.Skill);
            var awayTeamSkill = this.data.Players.Where(p => p.Team.Id == match.AwayTeamId).Sum(p => p.Skill);

            var luckFactor = RandomFactor();
            if (luckFactor % 2 == 0)
            {
                homeTeamSkill += luckFactor;
            }
            else
            {
                awayTeamSkill += luckFactor;
            }
            match.PlayedOn = DateTime.Now;
            match.HomeScore = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(homeTeamSkill))) / 2;
            match.AwayScore = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(awayTeamSkill))) / 2;

            AddResult(match.HomeTeam, this.GetResult(match.HomeScore, match.AwayScore, forHomeTeam: true));
            AddResult(match.AwayTeam, this.GetResult(match.HomeScore, match.AwayScore, forHomeTeam: false));

            GetLatestTeamRankings();

            await this.data.SaveChangesAsync();
            return match;
        }

        public async Task<Match> DeleteAsync(int id)
        {
            var match = await this.data.Matches.FindAsync(id);
            if (match == null)
            {
                return null;
            }
            this.data.Matches.Remove(match);
            await this.data.SaveChangesAsync();
            return match;
        }

        private int RandomFactor()
        {
            var random = new Random();

            return random.Next(1, 40);
        }
        private ResultType GetResult(int homeScore, int awayScore, bool forHomeTeam)
        {
            if (forHomeTeam)
            {
                var home = homeScore - awayScore;
                return this.ProcessResult(home);
            }

            var away = awayScore - homeScore;
            return this.ProcessResult(away);
        }
        private ResultType ProcessResult(int result)
        {
            if (result > 0)
            {
                return ResultType.Win;
            };

            return result == 0 ? ResultType.Draw : ResultType.Lose;
        }

        private void AddResult(Team team, ResultType result)
        {
            this.ResultActions[result].Invoke(team);
        }

        public void GetLatestTeamRankings()
        {
            var teams = this.data.Teams
                .OrderByDescending(t => t.Points)
                .ThenByDescending(t => t.Wins)
                .ToList();

            for (var i = 0; i < teams.Count; i++)
            {
                teams[i].Rank = i + 1;
            }

            this.data.SaveChanges();
        }
    }
}

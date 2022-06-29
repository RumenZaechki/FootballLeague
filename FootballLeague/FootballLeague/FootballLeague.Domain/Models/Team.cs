using FootballLeague.Domain.Models.Utility;

namespace FootballLeague.Domain.Models
{
    public class Team
    {
        private readonly List<Player> players;
        private readonly List<Match> homeMatches;
        private readonly List<Match> awayMatches;

        public Team() { }

        public Team(string name, IEnumerable<Player> players)
        {
            this.Name = name;
            this.players = players?.ToList() ?? new List<Player>();
            this.awayMatches = new List<Match>();
            this.homeMatches = new List<Match>();
        }

        public int Id { get; set; }
        public string Name { get; private set; }

        public int Wins { get; private set; }

        public int Draws { get; private set; }

        public int Loses { get; private set; }

        public int Points { get; private set; }

        public int Rank { get; private set; }

        public IReadOnlyCollection<Match> HomeMatchHistory => this.homeMatches.AsReadOnly();

        public IReadOnlyCollection<Match> AwayMatchHistory => this.awayMatches.AsReadOnly();

        public IReadOnlyCollection<Player> Players => this.players.AsReadOnly();

        public Team AddResult(ResultType result)
        {
            this.ResultActions[result].Invoke(this);

            return this;
        }

        public int GetTeamSkill()
        {
            return this.Players.Sum(p => p.Skill);
        }

        public void AddPlayer(Player player)
        {
            this.players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            this.players.Remove(player);
        }
        public Team UpdateName(string name)
        {
            this.Name = name;

            return this;
        }

        public Team UpdateRanking(int rank)
        {
            this.Rank = rank;

            return this;
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
                team.Loses++;
            }
        };

    }
}
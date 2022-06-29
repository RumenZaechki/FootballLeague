﻿using FootballLeague.Domain.Models.Utility;

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
        public string Name { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Losses { get; set; }

        public int Points { get; set; }

        public int Rank { get; set; }

        public IReadOnlyCollection<Match> HomeMatchHistory => this.homeMatches.AsReadOnly();

        public IReadOnlyCollection<Match> AwayMatchHistory => this.awayMatches.AsReadOnly();

        public IReadOnlyCollection<Player> Players => this.players?.AsReadOnly();

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

    }
}
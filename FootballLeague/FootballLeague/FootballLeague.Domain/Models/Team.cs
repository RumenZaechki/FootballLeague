using FootballLeague.Domain.Models.Utility;

namespace FootballLeague.Domain.Models
{
    public class Team
    {
        public Team() 
        {
            this.Players = new List<Player>();
            this.HomeMatches = new List<Match>();
            this.AwayMatches = new List<Match>();
        }

        public Team(string name)
        {
            this.Name = name;
            this.Players = new List<Player>();
            this.HomeMatches = new List<Match>();
            this.AwayMatches = new List<Match>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Losses { get; set; }

        public int Points { get; set; }

        public int Rank { get; set; }

        public List<Player> Players { get; set; }
        public List<Match> HomeMatches { get; set; }
        public List<Match> AwayMatches { get; set; }
    }
}
namespace FootballLeague.Domain.Models
{
    public class Match
    {
        public Match() { }

        public Match(Team homeTeam, Team awayTeam, string stadium)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Stadium = stadium;
        }

        public int Id { get; set; }
        public DateTime? PlayedOn { get; private set; }

        public Team HomeTeam { get; private set; }

        public int HomeTeamId { get; private set; }

        public Team AwayTeam { get; private set; }

        public int AwayTeamId { get; private set; }

        public int HomeScore { get; private set; }

        public int AwayScore { get; private set; }

        public string Stadium { get; private set; }
    }
}
namespace FootballLeague.Services.Models.Matches
{
    public class MatchServiceModel
    {
        public DateTime? PlayedOn { get; set; }

        public string HomeTeamName { get; set; }

        public int HomeTeamId { get; set; }

        public string AwayTeamName { get; set; }

        public int AwayTeamId { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        public string Stadium { get; set; }
    }
}

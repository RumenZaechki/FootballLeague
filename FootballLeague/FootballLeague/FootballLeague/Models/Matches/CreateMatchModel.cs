namespace FootballLeague.Models.Matches
{
    public class CreateMatchModel
    {
        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public string Stadium { get; set; }
    }
}

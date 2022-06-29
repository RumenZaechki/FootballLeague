using FootballLeague.Domain.Models.Utility;

namespace FootballLeague.Domain.Models
{
    public class Player
    {
        public Player() { }

        public Player(string firstName, string lastName, int skill, PositionType position, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Skill = skill;
            Position = position;
            BirthDate = birthDate;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public PositionType Position { get; set; }

        public int Skill { get; set; }

        public DateTime BirthDate { get; set; }

        public Team? Team { get; set; }
    }
}

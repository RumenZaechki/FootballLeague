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
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public PositionType Position { get; private set; }

        public int Skill { get; private set; }

        public DateTime BirthDate { get; private set; }

        public Team Team { get; private set; }

        public Player UpdateFirstName(string firstName)
        {
            this.FirstName = firstName;

            return this;
        }

        public Player UpdateLastName(string lastName)
        {
            this.LastName = lastName;

            return this;
        }

        public Player UpdatePosition(PositionType position)
        {
            this.Position = position;

            return this;
        }

        public Player UpdatePosition(string position)
        {
            Enum.TryParse(typeof(PositionType), position, true, out object result);
            this.Position = (PositionType)result;

            return this;
        }

        public Player UpdateTeam(Team team)
        {
            this.Team = team;

            return this;
        }

        public Player UpdateSkill(int skill)
        {
            this.Skill = skill;

            return this;
        }

    }
}

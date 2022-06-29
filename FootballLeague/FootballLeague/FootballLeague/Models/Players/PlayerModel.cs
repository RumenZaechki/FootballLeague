using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Models.Players
{
    public class PlayerModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public int Skill { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
        public int TeamId { get; set; }
    }
}

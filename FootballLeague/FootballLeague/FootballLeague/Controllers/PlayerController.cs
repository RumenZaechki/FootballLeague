using FootballLeague.Models.Players;
using FootballLeague.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;
        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        /// <summary>
        /// Create a new player
        /// </summary>
        /// <param name="player">
        /// Available positions = Goalkeeper, Defender, Midfield, Striker.
        /// Team ID is not required on creation</param>
        /// <returns>Player Object</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(PlayerModel player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdPlayer = await playerService
                .CreatePlayerAsync(player.FirstName, player.LastName, player.Position, player.Skill, player.BirthDate, player.TeamId);

            return CreatedAtAction(nameof(ReadOneAsync), createdPlayer);
        }

        /// <summary>
        /// Get details for a single player
        /// </summary>
        /// <param name="id">Player ID</param>
        /// <returns>Player Object</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadOneAsync(int id)
        {
            var player = await this.playerService.ReadOneAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        /// <summary>
        /// Get details for all players that are not in a team
        /// </summary>
        /// <returns>List of Player Objects</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ReadAllNotInTeamAsync()
        {
            var result = await this.playerService.ReadAllNotInTeamAsync();
            return Ok(result);
        }

        /// <summary>
        /// Update player details
        /// </summary>
        /// <param name="id">Player Id</param>
        /// <param name="player"></param>
        /// <returns>Player Object</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(int id, PlayerModel player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await this.playerService.UpdateAsync(id, player.FirstName, player.LastName, player.Position, player.Skill, player.BirthDate, player.TeamId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok();
        }

        /// <summary>
        /// Delete player
        /// </summary>
        /// <param name="id">Player ID</param>
        /// <returns>Player Object</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await this.playerService.DeleteAsync(id);
            //from what I've seen, returning player is not recommended in a delete method, but for some reason delete works only that way - I tried with a void method, it did nothing.
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}

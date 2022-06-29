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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ReadAllNotInTeamAsync()
        {
            var result = await this.playerService.ReadAllNotInTeamAsync();
            return Ok(result);
        }

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

using FootballLeague.Models.Matches;
using FootballLeague.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MatchController : Controller
    {
        private readonly IMatchService matchService;
        public MatchController(IMatchService matchService)
        {
            this.matchService = matchService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateAsync(CreateMatchModel match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdMatch = await this.matchService.CreateAsync(match.HomeTeamId, match.AwayTeamId, match.Stadium);
            if (createdMatch == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(ReadOneAsync), createdMatch);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadOneAsync(int id)
        {
            var result = await this.matchService.ReadOneAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ReadAllPlayedAsync()
        {
            var result = await this.matchService.ReadAllPlayedAsync();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PlayAsync(int id)
        {
            var result = await this.matchService.PlayAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await this.matchService.DeleteAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}

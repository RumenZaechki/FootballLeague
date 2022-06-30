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

        /// <summary>
        /// Create a match
        /// </summary>
        /// <param name="match"></param>
        /// <returns>Match Object</returns>
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

        /// <summary>
        /// Get details for a single match
        /// </summary>
        /// <param name="id">Match ID</param>
        /// <returns>Match Object</returns>
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

        /// <summary>
        /// Get details for all matches that are already played
        /// </summary>
        /// <returns>List of Match Objects</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ReadAllPlayedAsync()
        {
            var result = await this.matchService.ReadAllPlayedAsync();
            return Ok(result);
        }

        /// <summary>
        /// Play a match 
        /// </summary>
        /// <param name="id">Match ID</param>
        /// <returns>Match Object</returns>
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

        /// <summary>
        /// Delete a match
        /// </summary>
        /// <param name="id">Match ID</param>
        /// <returns>Match Object</returns>
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

using FootballLeague.Models.Teams;
using FootballLeague.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TeamController : Controller
    {
        private readonly ITeamService teamService;
        public TeamController(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        /// <summary>
        /// Create a team
        /// </summary>
        /// <param name="name">Name of Team</param>
        /// <returns>Team Object</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdTeam = await this.teamService.CreateAsync(name);
            return CreatedAtAction(nameof(ReadOneAsync), createdTeam);
        }

        /// <summary>
        /// Get details for a single team
        /// </summary>
        /// <param name="id">Team ID</param>
        /// <returns>Team Object</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadOneAsync(int id)
        {
            var result = await this.teamService.ReadOneAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Get details of all teams
        /// </summary>
        /// <returns>List of Team Objects</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ReadAllAsync()
        {
            var result = await this.teamService.ReadAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Update team details
        /// </summary>
        /// <param name="id">Team ID</param>
        /// <param name="team"></param>
        /// <returns>Team Object</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(int id, TeamModel team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await this.teamService.UpdateAssync(id, team.Name, team.Wins, team.Draws, team.Losses, team.Rank);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete team
        /// </summary>
        /// <param name="id">Team ID</param>
        /// <returns>Team Object</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await this.teamService.DeleteAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
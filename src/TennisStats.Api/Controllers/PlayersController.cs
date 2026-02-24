using Microsoft.AspNetCore.Mvc;
using TennisStats.Application.DTOs;
using TennisStats.Application.Interfaces;
using TennisStats.Domain.Entities;

namespace TennisStats.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return Ok(await _playerService.GetSortedPlayersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            // Note: The ExceptionMiddleware handles PlayerNotFoundException
            return Ok(await _playerService.GetPlayerByIdAsync(id));
        }

        [HttpGet("stats")]
        public async Task<ActionResult<GlobalStatsDto>> GetStats()
        {
            return Ok(await _playerService.GetGlobalStatsAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Player>> AddPlayer([FromBody] Player player)
        {
            if (player == null) return BadRequest();
            
            // Simple validation example
            if (string.IsNullOrEmpty(player.Firstname)) 
                return BadRequest(new { Message = "Firstname is required." });

            await _playerService.AddPlayerAsync(player);
            return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, player);
        }
    }
}

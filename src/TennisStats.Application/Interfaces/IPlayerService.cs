using TennisStats.Application.DTOs;
using TennisStats.Domain.Entities;

namespace TennisStats.Application.Interfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetSortedPlayersAsync();
        Task<Player?> GetPlayerByIdAsync(int id);
        Task<GlobalStatsDto> GetGlobalStatsAsync();
        Task AddPlayerAsync(Player player);
    }
}

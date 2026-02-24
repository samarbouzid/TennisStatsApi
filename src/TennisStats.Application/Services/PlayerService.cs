using TennisStats.Application.DTOs;
using TennisStats.Application.Interfaces;
using TennisStats.Application.Exceptions;
using TennisStats.Domain.Entities;
using TennisStats.Domain.Interfaces;

namespace TennisStats.Application.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<IEnumerable<Player>> GetSortedPlayersAsync()
        {
            var players = await _playerRepository.GetAllAsync();
            return players.OrderBy(p => p.Data?.Rank ?? int.MaxValue);
        }

        public async Task<Player?> GetPlayerByIdAsync(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if (player == null)
            {
                throw new PlayerNotFoundException(id);
            }
            return player;
        }

        public async Task<GlobalStatsDto> GetGlobalStatsAsync()
        {
            var players = (await _playerRepository.GetAllAsync()).ToList();
            if (!players.Any()) return new GlobalStatsDto();

            var bestCountry = players
                .GroupBy(p => p.Country?.Code)
                .Select(g => new
                {
                    CountryCode = g.Key,
                    WinRatio = g.Average(p => p.Data?.Last.Average(v => (double)v) ?? 0)
                })
                .OrderByDescending(x => x.WinRatio)
                .FirstOrDefault()?.CountryCode;

            var avgBMI = players
                .Where(p => p.Data != null && p.Data.Height > 0)
                .Average(p => (p.Data!.Weight / 1000.0) / Math.Pow(p.Data.Height / 100.0, 2));

            var heights = players
                .Where(p => p.Data != null)
                .Select(p => p.Data!.Height)
                .OrderBy(h => h)
                .ToList();

            double medianHeight = 0;
            if (heights.Any())
            {
                int count = heights.Count;
                if (count % 2 == 0)
                    medianHeight = (heights[count / 2 - 1] + heights[count / 2]) / 2.0;
                else
                    medianHeight = heights[count / 2];
            }

            return new GlobalStatsDto
            {
                BestCountryByWinRatio = bestCountry,
                AverageBMI = Math.Round(avgBMI, 2),
                MedianHeight = Math.Round(medianHeight, 2)
            };
        }

        public async Task AddPlayerAsync(Player player)
        {
            await _playerRepository.AddAsync(player);
        }
    }
}

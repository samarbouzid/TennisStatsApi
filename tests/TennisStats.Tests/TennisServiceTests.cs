using TennisStats.Application.Services;
using TennisStats.Infrastructure.Repositories;
using Xunit;

namespace TennisStats.Tests
{
    public class PlayerServiceTests
    {
        private readonly PlayerService _service;

        public PlayerServiceTests()
        {
            var repository = new JsonPlayerRepository();
            _service = new PlayerService(repository);
        }

        [Fact]
        public async Task GetSortedPlayersAsync_ShouldReturnPlayersByRank()
        {
            var players = (await _service.GetSortedPlayersAsync()).ToList();
            
            Assert.NotEmpty(players);
            for (int i = 0; i < players.Count - 1; i++)
            {
                Assert.True(players[i].Data?.Rank <= players[i + 1].Data?.Rank);
            }
        }

        [Fact]
        public async Task GetPlayerByIdAsync_ShouldReturnCorrectPlayer()
        {
            int testId = 52; // Novak
            var player = await _service.GetPlayerByIdAsync(testId);
            
            Assert.NotNull(player);
            Assert.Equal("Novak", player?.Firstname);
        }

        [Fact]
        public async Task GetGlobalStatsAsync_ShouldCalculateCorrectly()
        {
            var stats = await _service.GetGlobalStatsAsync();
            
            Assert.NotNull(stats);
            Assert.True(stats.AverageBMI > 0);
            Assert.True(stats.MedianHeight > 0);
            Assert.NotNull(stats.BestCountryByWinRatio);
        }
    }
}

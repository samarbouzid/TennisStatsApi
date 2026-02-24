using System.Text.Json;
using TennisStats.Domain.Entities;
using TennisStats.Domain.Interfaces;
using TennisStats.Infrastructure.Data;

namespace TennisStats.Infrastructure.Repositories
{
    public class JsonPlayerRepository : IPlayerRepository
    {
        private List<Player> _players = new();
        private readonly string _filePath;

        public JsonPlayerRepository()
        {
            _filePath = FindJsonFile();
            LoadData();
        }

        private string FindJsonFile()
        {
            var pathsToTry = new[]
            {
                Path.Combine(Directory.GetCurrentDirectory(), "Data", "headtohead.json"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "headtohead.json"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "TennisStats.Infrastructure", "Data", "headtohead.json"),
                Path.Combine(Directory.GetCurrentDirectory(), "src", "TennisStats.Infrastructure", "Data", "headtohead.json"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "src", "TennisStats.Infrastructure", "Data", "headtohead.json")
            };

            foreach (var path in pathsToTry)
            {
                if (File.Exists(path)) return path;
            }

            return "headtohead.json"; // Default fallback
        }

        private void LoadData()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                var root = JsonSerializer.Deserialize<PlayersRoot>(json);
                _players = root?.Players ?? new List<Player>();
            }
        }

        public Task<IEnumerable<Player>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Player>>(_players);
        }

        public Task<Player?> GetByIdAsync(int id)
        {
            return Task.FromResult(_players.FirstOrDefault(p => p.Id == id));
        }

        public Task AddAsync(Player player)
        {
            _players.Add(player);
            return Task.CompletedTask;
        }
    }
}

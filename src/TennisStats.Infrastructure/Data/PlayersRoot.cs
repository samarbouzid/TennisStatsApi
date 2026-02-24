using System.Text.Json.Serialization;
using TennisStats.Domain.Entities;

namespace TennisStats.Infrastructure.Data
{
    public class PlayersRoot
    {
        [JsonPropertyName("players")]
        public List<Player> Players { get; set; } = new();
    }
}

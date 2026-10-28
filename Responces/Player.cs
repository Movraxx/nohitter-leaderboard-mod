using System.Text.Json.Serialization;

namespace NohitterLeaderboard.Responces;

public class PlayerResponce
{
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }
    [JsonPropertyName("score")]
    public int Score { get; set; }
}
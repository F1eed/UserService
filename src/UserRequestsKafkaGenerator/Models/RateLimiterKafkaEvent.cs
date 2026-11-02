using System.Text.Json.Serialization;

namespace Itmo.HighLoad.UserRequestsKafkaGenerator.Models;

public record RateLimiterKafkaEvent
{
    public RateLimiterKafkaEvent(UserId userId, Endpoint endpoint)
    {
        UserId = userId.Value;
        Endpoint = endpoint.Value;
    }

    [property: JsonPropertyName("user_id")]
    public long UserId { get; }

    [property: JsonPropertyName("endpoint")]
    public string Endpoint { get; }
}

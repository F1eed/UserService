using System.Text.Json.Serialization;

namespace RateLimiter.Reader.KafkaConsumer.Models;

public sealed record RateLimiterKafkaEvent(
    [property: JsonPropertyName("user_id")]
    long UserId,

    [property: JsonPropertyName("endpoint")]
    string Endpoint);
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RateLimiter.Reader.Infrastructure.Entities;

public sealed class RateLimitEntity
{
    [BsonId]
    public ObjectId Id { get; init; }

    [BsonElement("route")]
    public required string Route { get; init; }

    [BsonElement("requests_per_minute")]
    public required int RequestsPerMinute { get; init; }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RateLimiter.Writer.Infrastructure.Entities;

public sealed class RateLimitEntity
{
    [BsonId]
    public ObjectId Id { get; init; }

    [BsonElement("route")]
    public required string Route { get; init; }

    [BsonElement("requests_per_minute")]
    public required long RequestsPerMinute { get; init; }
}
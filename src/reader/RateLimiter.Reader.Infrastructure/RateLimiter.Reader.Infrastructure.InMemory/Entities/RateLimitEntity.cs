namespace RateLimiter.Reader.Infrastructure.InMemory.Entities;

public sealed class RateLimitEntity
{
    public required string Route { get; init; }

    public required int RequestsPerMinute { get; init; }
}
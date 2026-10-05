namespace RateLimiter.Writer.Infrastructure.Options;

public sealed class MongoConnectionOptions
{
    public required string ConnectionString { get; init; }

    public required string DatabaseName { get; init; }
}
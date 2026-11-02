using System.ComponentModel.DataAnnotations;

namespace RateLimiter.Reader.Infrastructure.Options;

public sealed class MongoConnectionOptions
{
    [MinLength(1)]
    public required string Host { get; init; } = string.Empty;

    [Range(minimum: 1, maximum: 65535)]
    public required int Port { get; init; }

    public required string Database { get; init; } = string.Empty;

    public string ToConnectionString() => $"mongodb://{Host}:{Port}/{Database}";
}
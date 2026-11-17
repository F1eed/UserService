using System.ComponentModel.DataAnnotations;

namespace RateLimiter.Reader.RateLimitsStatisticsRepository.Options;

public sealed class RedisConnectionOptions
{
    [MinLength(1)]
    public required string Host { get; init; } = string.Empty;

    [Range(1, 65535)]
    public required int Port { get; init; } = 6379;

    public string ToConnectionString()
    {
        return $"{Host}:{Port}";
    }
}
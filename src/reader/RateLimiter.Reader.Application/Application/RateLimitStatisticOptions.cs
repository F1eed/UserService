namespace RateLimiter.Reader.Application.Application;

public sealed class RateLimitStatisticOptions
{
    public required TimeSpan Window { get; init; }

    public required TimeSpan Ban { get; init; }
}
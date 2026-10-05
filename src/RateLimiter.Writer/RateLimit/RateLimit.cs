using Itmo.HighLoad.RateLimiter.Writer.RateLimit.ValueObjects;

namespace Itmo.HighLoad.RateLimiter.Writer.RateLimit;

public sealed class RateLimit
{
    public RequestsPerMinute RequestsPerMinute { get; }

    public string Route { get; }

    public RateLimit(RequestsPerMinute requestsPerMinute, string route)
    {
        RequestsPerMinute = requestsPerMinute;
        Route = route;
    }
}
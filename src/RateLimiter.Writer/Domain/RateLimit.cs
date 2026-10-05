using Itmo.HighLoad.RateLimiter.Writer.Domain.ValueObjects;

namespace Itmo.HighLoad.RateLimiter.Writer.Domain;

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
using RateLimiter.Reader.Application.Models.ValueObjects;

namespace RateLimiter.Reader.Application.Models;

public sealed class RateLimit
{
    public RequestsPerMinute RequestsPerMinute { get; }

    public string Route { get; }

    public RateLimit(string route, RequestsPerMinute requestsPerMinute)
    {
        Route = route;
        RequestsPerMinute = requestsPerMinute;
    }
}

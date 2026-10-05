using RateLimiter.Writer.Application.Models.ValueObjects;

namespace RateLimiter.Writer.Application.Models;

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
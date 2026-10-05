using RateLimiter.Writer.Application.Domain.ValueObjects;

namespace RateLimiter.Writer.Application.Domain;

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
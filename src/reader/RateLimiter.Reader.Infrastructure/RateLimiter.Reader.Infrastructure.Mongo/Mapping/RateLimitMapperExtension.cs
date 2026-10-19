using RateLimiter.Reader.Application.Models;
using RateLimiter.Reader.Application.Models.ValueObjects;
using RateLimiter.Reader.Infrastructure.Entities;

namespace RateLimiter.Reader.Infrastructure.Mapping;

public static class RateLimitMapperExtension
{
    public static RateLimit ToDomain(this RateLimitEntity entry)
    {
        return new RateLimit(
            entry.Route,
            new RequestsPerMinute(entry.RequestsPerMinute));
    }
}
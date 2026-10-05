using RateLimiter.Writer.Application.Models;
using RateLimiter.Writer.Application.Models.ValueObjects;
using RateLimiter.Writer.Infrastructure.Entities;

namespace RateLimiter.Writer.Infrastructure;

public static class RateLimitMapper
{
    public static RateLimit ToDomain(RateLimitEntity entity)
    {
        return new RateLimit(
            entity.Route,
            RequestsPerMinute.Create(entity.RequestsPerMinute));
    }

    public static RateLimitEntity ToEntity(RateLimit rateLimit)
    {
        return new RateLimitEntity
        {
            Route = rateLimit.Route,
            RequestsPerMinute = rateLimit.RequestsPerMinute.Value,
        };
    }
}
using RateLimiter.Writer.Application.Contracts.Models;
using RateLimiter.Writer.Application.Models;

namespace RateLimiter.Writer.Application.Application.Mapping;

public static class WriterMappingExtensions
{
    public static RateLimitDto MapToDto(this RateLimit limit)
        => new RateLimitDto(limit.Route, limit.RequestsPerMinute.Value);
}
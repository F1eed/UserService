using RateLimiter.Writer.Application.Contracts.Models;

namespace RateLimiter.Writer.Presentation.Mapping;

public static class WriterMappingExtension
{
    public static RateLimit MapToProto(this RateLimitDto dto)
        => new RateLimit
        {
            RequestsPerMinute = dto.RequestsPerMinute,
            Route = dto.Route,
        };
}
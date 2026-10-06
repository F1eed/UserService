namespace RateLimiter.Writer.Application.Contracts.Models;

public record RateLimitDto(string Route, int RequestsPerMinute);
namespace RateLimiter.Writer.Application.Abstractions.Persistence.Queries;

public sealed record UpdateQuery(string Route, int RequestsPerMinute);
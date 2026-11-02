namespace RateLimiter.Reader.Application.Abstractions.Redis.Repositories;

public interface IRateLimitStatisticsRepository
{
    Task ProcessRequestAsync(
        long userId,
        string route,
        int requestsPerMinute,
        TimeSpan window,
        TimeSpan ban,
        CancellationToken cancellationToken);
}
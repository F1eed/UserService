namespace RateLimiter.Reader.Application.Abstractions.Redis.Repositories;

public interface IRateLimitStatisticsRepository
{
    Task RegisterRequestAsync(
        long userId,
        string route,
        int requestsPerMinute,
        TimeSpan window,
        TimeSpan ban,
        CancellationToken cancellationToken);

    Task<bool> IsBannedAsync(
        long userId,
        CancellationToken cancellationToken);
}
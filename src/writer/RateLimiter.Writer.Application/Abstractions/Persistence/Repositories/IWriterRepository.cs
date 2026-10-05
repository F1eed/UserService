using RateLimiter.Writer.Application.Models;
using RateLimiter.Writer.Application.Models.ValueObjects;

namespace RateLimiter.Writer.Application.Abstractions.Persistence.Repositories;

public interface IWriterRepository
{
    Task<RateLimit> AddLimitAsync(
        RateLimit rateLimit,
        CancellationToken cancellationToken);

    Task<RateLimit?> QueryByRouteAsync(
        string route,
        CancellationToken cancellationToken);

    Task<bool> UpdateLimitAsync(
        string route,
        RequestsPerMinute? rateLimit,
        CancellationToken cancellationToken);

    Task DeleteLimitAsync(
        string route,
        CancellationToken cancellationToken);
}
using RateLimiter.Writer.Application.Contracts.Operations;

namespace RateLimiter.Writer.Application.Contracts;

public interface IWriteService
{
    Task<CreateLimit.Response> CreateLimitAsync(
        CreateLimit.Request createLimitRequest,
        CancellationToken cancellationToken);

    Task<GetLimitByRoute.Response> GetLimitByRouteAsync(
        GetLimitByRoute.Request getLimitByRouteRequest,
        CancellationToken cancellationToken);

    Task<UpdateLimit.Response> UpdateLimitAsync(
        UpdateLimit.Request updateLimitRequest,
        CancellationToken cancellationToken);

    Task DeleteLimitAsync(
        DeleteLimit.Request deleteLimitRequest,
        CancellationToken cancellationToken);
}
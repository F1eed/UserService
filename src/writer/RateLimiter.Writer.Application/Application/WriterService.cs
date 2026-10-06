using RateLimiter.Writer.Application.Abstractions.Persistence.Queries;
using RateLimiter.Writer.Application.Abstractions.Persistence.Repositories;
using RateLimiter.Writer.Application.Application.Mapping;
using RateLimiter.Writer.Application.Contracts;
using RateLimiter.Writer.Application.Contracts.Operations;
using RateLimiter.Writer.Application.Models;
using RateLimiter.Writer.Application.Models.ValueObjects;

namespace RateLimiter.Writer.Application.Application;

public class WriterService(IWriterRepository writerRepository) : IWriterService
{
    public async Task<CreateLimit.Response> CreateLimitAsync(
        CreateLimit.Request createLimitRequest,
        CancellationToken cancellationToken)
    {
        var rateLimit = new RateLimit(
            createLimitRequest.RateLimit.Route,
            RequestsPerMinute.Create(createLimitRequest.RateLimit.RequestsPerMinute));

        RateLimit result = await writerRepository.AddLimitAsync(
            rateLimit,
            cancellationToken);

        return new CreateLimit.Response.Success(result.MapToDto());
    }

    public async Task<GetLimitByRoute.Response> GetLimitByRouteAsync(
        GetLimitByRoute.Request getLimitByRouteRequest,
        CancellationToken cancellationToken)
    {
        RateLimit? result = await writerRepository.QueryByRouteAsync(
            getLimitByRouteRequest.Route,
            cancellationToken);

        if (result is null)
        {
            return new GetLimitByRoute.Response.Failure("Route is not found.");
        }

        return new GetLimitByRoute.Response.Success(result.MapToDto());
    }

    public async Task<UpdateLimit.Response> UpdateLimitAsync(
        UpdateLimit.Request updateLimitRequest,
        CancellationToken cancellationToken)
    {
        var rateLimit = new RateLimit(
            updateLimitRequest.Route,
            RequestsPerMinute.Create(updateLimitRequest.RequestsPerMinute));

        if (await writerRepository.UpdateLimitAsync(
                new UpdateQuery(rateLimit.Route, rateLimit.RequestsPerMinute.Value),
                cancellationToken))
        {
            return new UpdateLimit.Response.Success("Success update.");
        }

        return new UpdateLimit.Response.NotFound();
    }

    public async Task DeleteLimitAsync(
        DeleteLimit.Request deleteLimitRequest,
        CancellationToken cancellationToken)
    {
        await writerRepository.DeleteLimitAsync(deleteLimitRequest.Route, cancellationToken);
    }
}
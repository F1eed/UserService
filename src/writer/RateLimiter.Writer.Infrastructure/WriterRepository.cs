using MongoDB.Driver;
using RateLimiter.Writer.Application.Abstractions.Persistence.Queries;
using RateLimiter.Writer.Application.Abstractions.Persistence.Repositories;
using RateLimiter.Writer.Application.Models;
using RateLimiter.Writer.Infrastructure.Entities;
using RateLimiter.Writer.Infrastructure.Mapping;

namespace RateLimiter.Writer.Infrastructure;

public sealed class WriterRepository : IWriterRepository
{
    private readonly IMongoCollection<RateLimitEntity> _rateLimitCollection;

    public WriterRepository(MongoContext context)
    {
        _rateLimitCollection = context.RateLimits;
    }

    public async Task<RateLimit> AddLimitAsync(
        RateLimit rateLimit,
        CancellationToken cancellationToken)
    {
        RateLimitEntity entity = new()
        {
            Route = rateLimit.Route,
            RequestsPerMinute = rateLimit.RequestsPerMinute.Value,
        };

        try
        {
            await _rateLimitCollection.InsertOneAsync(
                entity,
                cancellationToken: cancellationToken);
        }
        catch (MongoDuplicateKeyException ex)
        {
            throw new ArgumentException(ex.Message);
        }

        return rateLimit;
    }

    public async Task<RateLimit?> QueryByRouteAsync(
        string route,
        CancellationToken cancellationToken)
    {
        RateLimitEntity? entity = await _rateLimitCollection
            .Find(x => x.Route == route)
            .FirstOrDefaultAsync(cancellationToken);

        return entity == null ? null : RateLimitMapper.ToDomain(entity);
    }

    public async Task<bool> UpdateLimitAsync(
        UpdateQuery updateQuery,
        CancellationToken cancellationToken)
    {
        UpdateResult result = await _rateLimitCollection.UpdateOneAsync(
            x => x.Route == updateQuery.Route,
            Builders<RateLimitEntity>.Update
                .Set(
                    x => x.RequestsPerMinute,
                    updateQuery.RequestsPerMinute),
            cancellationToken: cancellationToken);

        return result.MatchedCount > 0;
    }

    public async Task DeleteLimitAsync(
        string route,
        CancellationToken cancellationToken)
    {
        await _rateLimitCollection.DeleteOneAsync(
            x => x.Route == route,
            cancellationToken);
    }
}
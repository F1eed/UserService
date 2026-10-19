using MongoDB.Driver;
using RateLimiter.Reader.Application.Abstractions.Mongo.Repositories;
using RateLimiter.Reader.Application.Models;
using RateLimiter.Reader.Infrastructure.Entities;
using RateLimiter.Reader.Infrastructure.Mapping;

namespace RateLimiter.Reader.Infrastructure;

public sealed class ReaderMongoRepository : IReaderMongoRepository
{
    private const int DefaultBatchSize = 1000;
    private readonly IMongoCollection<RateLimitEntity> _collection;

    public ReaderMongoRepository(IMongoCollection<RateLimitEntity> collection)
    {
        _collection = collection;
    }

    public async IAsyncEnumerable<RateLimit> LoadLimitsAsync()
    {
        var options = new FindOptions<RateLimitEntity>
        {
            BatchSize = DefaultBatchSize,
        };

        using IAsyncCursor<RateLimitEntity> cursor = await _collection.FindAsync(
            FilterDefinition<RateLimitEntity>.Empty,
            options);

        await foreach (RateLimitEntity entry in cursor.ToAsyncEnumerable())
        {
            yield return entry.ToDomain();
        }
    }

    public async IAsyncEnumerable<RateLimit> WatchLimitsAsync()
    {
        PipelineDefinition<ChangeStreamDocument<RateLimitEntity>, ChangeStreamDocument<RateLimitEntity>>
            pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<RateLimitEntity>>()
                .Match(change => change.OperationType == ChangeStreamOperationType.Update);

        using IChangeStreamCursor<ChangeStreamDocument<RateLimitEntity>> cursor
            = await _collection.WatchAsync(pipeline);

        await foreach (ChangeStreamDocument<RateLimitEntity>? entry in cursor.ToAsyncEnumerable())
        {
            yield return entry.FullDocument.ToDomain();
        }
    }
}

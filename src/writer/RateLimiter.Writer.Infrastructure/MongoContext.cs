using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RateLimiter.Writer.Infrastructure.Entities;
using RateLimiter.Writer.Infrastructure.Options;

namespace RateLimiter.Writer.Infrastructure;

public sealed class MongoContext
{
    public IMongoCollection<RateLimitEntity> RateLimits { get; }

    public MongoContext(IOptions<MongoConnectionOptions> options)
    {
        MongoConnectionOptions mongoOptions = options.Value;

        var client = new MongoClient(mongoOptions.ToConnectionString());

        IMongoDatabase database = client.GetDatabase(mongoOptions.Database);

        RateLimits = database.GetCollection<RateLimitEntity>("rate_limits");

        IndexKeysDefinition<RateLimitEntity> indexDefinition = Builders<RateLimitEntity>
            .IndexKeys.Ascending(x => x.Route);

        var indexOptions = new CreateIndexOptions { Unique = true, };

        var indexModel = new CreateIndexModel<RateLimitEntity>(indexDefinition, indexOptions);

        RateLimits.Indexes.CreateOne(indexModel);
    }
}
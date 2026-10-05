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

        MongoClient client = new(mongoOptions.ConnectionString);

        IMongoDatabase database = client.GetDatabase(mongoOptions.DatabaseName);

        RateLimits = database.GetCollection<RateLimitEntity>("rate_limits");
    }
}
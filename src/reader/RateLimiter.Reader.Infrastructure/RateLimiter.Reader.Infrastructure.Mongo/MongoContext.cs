using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RateLimiter.Reader.Infrastructure.Entities;
using RateLimiter.Reader.Infrastructure.Options;

namespace RateLimiter.Reader.Infrastructure;

public sealed class MongoContext
{
    public IMongoCollection<RateLimitEntity> RateLimits { get; }

    public MongoContext(IOptions<MongoConnectionOptions> options)
    {
        MongoConnectionOptions mongoOptions = options.Value;

        var client = new MongoClient(mongoOptions.ToConnectionString());

        IMongoDatabase database = client.GetDatabase(mongoOptions.Database);

        RateLimits = database.GetCollection<RateLimitEntity>("rate_limits");
    }
}

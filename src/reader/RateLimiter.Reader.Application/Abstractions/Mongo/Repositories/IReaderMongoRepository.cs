using RateLimiter.Reader.Application.Models;

namespace RateLimiter.Reader.Application.Abstractions.Mongo.Repositories;

public interface IReaderMongoRepository
{
    IAsyncEnumerable<RateLimit> LoadLimitsAsync();

    IAsyncEnumerable<RateLimit> WatchLimitsAsync();
}
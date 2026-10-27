using RateLimiter.Reader.Application.Abstractions.InMemory.Repositories;
using RateLimiter.Reader.Application.Abstractions.Mongo.Repositories;

namespace RateLimiter.Reader.Application.Abstractions;

public interface IPersistenceContext
{
    IReaderMongoRepository MongoRepository { get; }

    IReaderInMemoryRepository InMemoryRepository { get; }
}
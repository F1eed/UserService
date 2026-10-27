using RateLimiter.Reader.Application.Abstractions;
using RateLimiter.Reader.Application.Abstractions.InMemory.Repositories;
using RateLimiter.Reader.Application.Abstractions.Mongo.Repositories;

namespace RateLimiter.Reader.Infrastructure;

public class PersistenceContext : IPersistenceContext
{
    public PersistenceContext(
        IReaderMongoRepository mongoRepository,
        IReaderInMemoryRepository inMemoryRepository)
    {
        MongoRepository = mongoRepository;
        InMemoryRepository = inMemoryRepository;
    }

    public IReaderMongoRepository MongoRepository { get; }

    public IReaderInMemoryRepository InMemoryRepository { get; }
}
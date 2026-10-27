using RateLimiter.Reader.Application.Models;

namespace RateLimiter.Reader.Application.Abstractions.InMemory.Repositories;

public interface IReaderInMemoryRepository
{
    void AddLimit(RateLimit limit);

    bool TryUpdateLimit(RateLimit limit);

    IEnumerable<RateLimit> GetAllLimits();
}
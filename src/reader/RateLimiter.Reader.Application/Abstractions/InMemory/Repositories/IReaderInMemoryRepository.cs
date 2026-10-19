using RateLimiter.Reader.Application.Models;

namespace RateLimiter.Reader.Application.Abstractions.InMemory.Repositories;

public interface IReaderInMemoryRepository
{
    bool TryAddLimit(RateLimit limit);

    bool TryUpdateLimit(RateLimit limit);

    IEnumerable<RateLimit> GetAllLimits();
}
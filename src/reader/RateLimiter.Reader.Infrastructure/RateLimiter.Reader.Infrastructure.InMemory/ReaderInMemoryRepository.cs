using RateLimiter.Reader.Application.Abstractions.InMemory.Repositories;
using RateLimiter.Reader.Application.Models;
using RateLimiter.Reader.Application.Models.ValueObjects;
using System.Collections.Concurrent;

namespace RateLimiter.Reader.Infrastructure.InMemory;

public sealed class ReaderInMemoryRepository : IReaderInMemoryRepository
{
    private readonly ConcurrentDictionary<string, int> _values = new();

    public bool TryAddLimit(RateLimit limit)
    {
        return _values.TryAdd(limit.Route, limit.RequestsPerMinute.Value);
    }

    public bool TryUpdateLimit(RateLimit limit)
    {
        return _values.TryUpdate(
            limit.Route,
            limit.RequestsPerMinute.Value,
            _values[limit.Route]);
    }

    public IEnumerable<RateLimit> GetAllLimits()
    {
        foreach (KeyValuePair<string, int> entry in _values)
        {
            yield return new RateLimit(entry.Key, new RequestsPerMinute(entry.Value));
        }
    }
}
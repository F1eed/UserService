using RateLimiter.Reader.Application.Abstractions.InMemory.Repositories;
using RateLimiter.Reader.Application.Models;
using RateLimiter.Reader.Application.Models.ValueObjects;
using System.Collections.Concurrent;

namespace RateLimiter.Reader.Infrastructure.InMemory;

public sealed class ReaderInMemoryRepository : IReaderInMemoryRepository
{
    private readonly ConcurrentDictionary<string, int> _values = new();

    public void AddLimit(RateLimit limit)
    {
        _values.TryAdd(limit.Route, limit.RequestsPerMinute.Value);
    }

    public bool TryUpdateLimit(RateLimit limit)
    {
        if (_values.ContainsKey(limit.Route))
        {
            _values[limit.Route] = limit.RequestsPerMinute.Value;
            return true;
        }

        return false;
    }

    public IEnumerable<RateLimit> GetAllLimits()
    {
        foreach (KeyValuePair<string, int> entry in _values)
        {
            yield return new RateLimit(entry.Key, new RequestsPerMinute(entry.Value));
        }
    }
}
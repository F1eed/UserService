using RateLimiter.Reader.Application.Abstractions;
using RateLimiter.Reader.Application.Contracts;
using RateLimiter.Reader.Application.Models;

namespace RateLimiter.Reader.Application.Application;

public class ReaderService : IReaderService
{
    private readonly IPersistenceContext _context;

    public ReaderService(IPersistenceContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync()
    {
        await foreach (RateLimit limit in _context.MongoRepository.LoadLimitsAsync())
        {
            _context.InMemoryRepository.AddLimit(limit);
        }

        _ = Task.Factory
            .StartNew(async () =>
            {
                await foreach (RateLimit limit in _context.MongoRepository.WatchLimitsAsync())
                {
                    if (_context.InMemoryRepository.TryUpdateLimit(limit) is false)
                    {
                        throw new InvalidOperationException($"Cannot add limit for {limit.Route}");
                    }
                }
            }).Unwrap();
    }

    public IEnumerable<RateLimit> GetAllLimits()
    {
        return _context.InMemoryRepository.GetAllLimits();
    }
}

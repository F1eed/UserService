using RateLimiter.Reader.Application.Models;

namespace RateLimiter.Reader.Application.Abstractions.Persistence.Repositories;

public interface IReaderRepository
{
    Task LoadLimits();

    IAsyncEnumerable<RateLimit> GetAllLimits();
}
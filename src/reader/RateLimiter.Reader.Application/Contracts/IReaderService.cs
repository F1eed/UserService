using RateLimiter.Reader.Application.Models;

namespace RateLimiter.Reader.Application.Contracts;

public interface IReaderService
{
    Task InitializeAsync();

    IEnumerable<RateLimit> GetAllLimits();
}
using RateLimiter.Reader.Application.Abstractions;
using RateLimiter.Reader.Application.Contracts;
using RateLimiter.Reader.Application.Contracts.Models;

namespace RateLimiter.Reader.Application.Application;

public class ReaderService : IReaderService
{
    private readonly IPersistenceContext _context;

    public ReaderService(IPersistenceContext context)
    {
        _context = context;
    }

    public GetAllLimits.Result GetAllLimits()
    {
        return new GetAllLimits.Result.Success(_context.InMemoryRepository.GetAllLimits());
    }
}

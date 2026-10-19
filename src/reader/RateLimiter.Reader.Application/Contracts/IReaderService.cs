using RateLimiter.Reader.Application.Contracts.Models;

namespace RateLimiter.Reader.Application.Contracts;

public interface IReaderService
{
    GetAllLimits.Result GetAllLimits();
}
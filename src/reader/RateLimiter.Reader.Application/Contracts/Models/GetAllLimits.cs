using RateLimiter.Reader.Application.Models;

namespace RateLimiter.Reader.Application.Contracts.Models;

public static class GetAllLimits
{
    public abstract record Result
    {
        private Result() { }

        public sealed record Success(IEnumerable<RateLimit> Limits) : Result;
    }
}
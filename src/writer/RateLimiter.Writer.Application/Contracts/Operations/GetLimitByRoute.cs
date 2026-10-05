using RateLimiter.Writer.Application.Contracts.Models;

namespace RateLimiter.Writer.Application.Contracts.Operations;

public static class GetLimitByRoute
{
    public readonly record struct Request(string Route);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(RateLimitDto RateLimit) : Response;

        public sealed record Failure(string ErrorMessage) : Response;
    }
}
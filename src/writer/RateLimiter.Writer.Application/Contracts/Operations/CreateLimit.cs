using RateLimiter.Writer.Application.Contracts.Models;

namespace RateLimiter.Writer.Application.Contracts.Operations;

public static class CreateLimit
{
    public readonly record struct Request(RateLimitEntry RateLimit);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(RateLimitDto RateLimit) : Response;
    }

    public record RateLimitEntry(string Route, int RequestsPerMinute);
}
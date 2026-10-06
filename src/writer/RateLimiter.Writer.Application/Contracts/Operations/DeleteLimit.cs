namespace RateLimiter.Writer.Application.Contracts.Operations;

public static class DeleteLimit
{
    public readonly record struct Request(string Route);
}
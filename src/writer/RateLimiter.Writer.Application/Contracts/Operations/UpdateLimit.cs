namespace RateLimiter.Writer.Application.Contracts.Operations;

public static class UpdateLimit
{
    public readonly record struct Request(
        string Route,
        long? RequestsPerMinute);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(string SuccessMessage) : Response;

        public sealed record NotFound : Response;
    }
}
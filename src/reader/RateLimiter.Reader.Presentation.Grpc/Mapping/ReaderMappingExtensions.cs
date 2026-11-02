namespace RateLimiter.Reader.Presentation.Mapping;

public static class ReaderMappingExtensions
{
    public static RateLimit ToProto(
        this Application.Models.RateLimit limit)
    {
        return new RateLimit
        {
            Route = limit.Route,
            RequestsPerMinute = limit.RequestsPerMinute.Value,
        };
    }
}
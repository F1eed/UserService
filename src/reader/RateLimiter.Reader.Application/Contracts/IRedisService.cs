namespace RateLimiter.Reader.Application.Contracts;

public interface IRedisService
{
    Task ProcessRequestAsync(long userId, string endpoint);
}
namespace RateLimiter.Reader.Application.Contracts;

public interface IRedisService
{
    Task ProcessRequestAsync(int userId, string endpoint);
}
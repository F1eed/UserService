using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RateLimiter.Reader.Application.Application;
using RateLimiter.Reader.Application.Contracts;

namespace RateLimiter.Reader.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection collection,
        IConfiguration configuration)
    {
        collection.AddSingleton<IReaderService, ReaderService>();
        collection.AddSingleton<IRedisService, RedisService>();
        collection.AddHostedService<HostedService>();

        collection.AddOptions<RateLimitStatisticOptions>()
            .Bind(configuration.GetSection("RateLimitStatisticsOptions"));

        return collection;
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RateLimiter.Reader.Application.Abstractions.Redis.Repositories;
using RateLimiter.Reader.RateLimitsStatisticsRepository.Options;
using StackExchange.Redis;

namespace RateLimiter.Reader.RateLimitsStatisticsRepository;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRedis(
        this IServiceCollection collection,
        IConfiguration configuration)
    {
        IConfigurationSection redisSection = configuration.GetSection("Infrastructure:Persistence:Redis");

        collection.AddOptions<RedisConnectionOptions>()
            .BindConfiguration("Infrastructure:Persistence:Redis")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        RedisConnectionOptions options = redisSection.Get<RedisConnectionOptions>()
                      ?? throw new InvalidOperationException("Redis configuration is not found");

        string connectionString = options.ToConnectionString();

        collection.AddSingleton(_ => ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            EndPoints = { connectionString },
        }));

        collection.AddSingleton<IRateLimitStatisticsRepository, RateLimitStatisticsRepository>();

        return collection;
    }
}
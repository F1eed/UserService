using Itmo.HighLoad.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace UserService.Infrastructure.UserBanned;

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

        collection.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            EndPoints = { connectionString },
        }));

        collection.AddSingleton<IUserBanRepository, UserBanRepository>();

        return collection;
    }
}
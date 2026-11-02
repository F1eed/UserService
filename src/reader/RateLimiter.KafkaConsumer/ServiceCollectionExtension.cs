using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RateLimiter.Reader.KafkaConsumer.Consumer;
using RateLimiter.Reader.KafkaConsumer.Options;

namespace RateLimiter.Reader.KafkaConsumer;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddKafkaConsumer(
        this IServiceCollection collection,
        IConfiguration configuration)
    {
        collection
            .AddOptions<KafkaOptions>()
            .Bind(configuration.GetSection("Kafka:RateLimiter"))
            .ValidateOnStart();

        collection.AddHostedService<UserRequestConsumer>();

        return collection;
    }
}
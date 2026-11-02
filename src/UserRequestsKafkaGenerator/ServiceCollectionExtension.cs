using Itmo.HighLoad.UserRequestsKafkaGenerator.Generator;
using Itmo.HighLoad.UserRequestsKafkaGenerator.Models;
using Itmo.HighLoad.UserRequestsKafkaGenerator.Options;
using Itmo.HighLoad.UserRequestsKafkaGenerator.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.HighLoad.UserRequestsKafkaGenerator;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddKafkaGenerator(
        this IServiceCollection collection,
        IConfiguration configuration)
    {
        collection
            .AddOptions<KafkaOptions>()
            .Bind(configuration.GetSection("Kafka:RateLimiter"))
            .ValidateOnStart();

        collection
            .AddSingleton<
                IKafkaProducer<RateLimiterKafkaEvent>,
                KafkaProducer<RateLimiterKafkaEvent>>()
            .AddSingleton<GeneratorManager>();

        return collection;
    }
}
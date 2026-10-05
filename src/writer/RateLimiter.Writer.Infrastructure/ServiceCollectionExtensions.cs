using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RateLimiter.Writer.Application.Abstractions.Persistence.Repositories;
using RateLimiter.Writer.Infrastructure.Options;

namespace RateLimiter.Writer.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructurePersistence(
        this IServiceCollection collection,
        IConfiguration configuration)
    {
        IConfigurationSection mongoSection = configuration.GetSection("Infrastructure:Persistence:Mongo");

        collection.AddOptions<MongoConnectionOptions>()
            .Bind(mongoSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        collection.AddSingleton<MongoContext>();
        collection.AddSingleton<IWriterRepository, WriterRepository>();

        return collection;
    }
}
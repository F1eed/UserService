using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RateLimiter.Reader.Application.Abstractions.Mongo.Repositories;
using RateLimiter.Reader.Infrastructure.Options;

namespace RateLimiter.Reader.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoInfrastructure(
        this IServiceCollection collection,
        IConfiguration configuration)
    {
        collection.AddOptions<MongoConnectionOptions>()
            .BindConfiguration("Infrastructure:Persistence:Mongo")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        collection.AddSingleton<MongoContext>();
        collection.AddSingleton<IReaderMongoRepository, ReaderMongoRepository>();

        return collection;
    }
}
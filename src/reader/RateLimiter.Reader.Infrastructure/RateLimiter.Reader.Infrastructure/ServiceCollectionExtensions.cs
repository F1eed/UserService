using Microsoft.Extensions.DependencyInjection;
using RateLimiter.Reader.Application.Abstractions;
using RateLimiter.Reader.Infrastructure.InMemory;

namespace RateLimiter.Reader.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceContext(this IServiceCollection collection)
    {
        collection
            .AddInMemoryInfrastructure()
            .AddMongoInfrastructure();

        collection.AddSingleton<IPersistenceContext, PersistenceContext>();

        return collection;
    }
}
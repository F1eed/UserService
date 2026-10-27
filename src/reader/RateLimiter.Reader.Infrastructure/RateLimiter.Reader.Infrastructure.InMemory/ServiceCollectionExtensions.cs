using Microsoft.Extensions.DependencyInjection;
using RateLimiter.Reader.Application.Abstractions.InMemory.Repositories;

namespace RateLimiter.Reader.Infrastructure.InMemory;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInMemoryInfrastructure(this IServiceCollection collection)
    {
        collection.AddSingleton<IReaderInMemoryRepository, ReaderInMemoryRepository>();

        return collection;
    }
}
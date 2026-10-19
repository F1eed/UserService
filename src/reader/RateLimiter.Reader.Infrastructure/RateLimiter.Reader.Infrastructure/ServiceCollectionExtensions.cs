using Microsoft.Extensions.DependencyInjection;
using RateLimiter.Reader.Application.Abstractions;

namespace RateLimiter.Reader.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceContext(this IServiceCollection collection)
    {
        collection.AddSingleton<IPersistenceContext, PersistenceContext>();

        return collection;
    }
}
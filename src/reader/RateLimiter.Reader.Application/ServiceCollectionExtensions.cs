using Microsoft.Extensions.DependencyInjection;
using RateLimiter.Reader.Application.Application;
using RateLimiter.Reader.Application.Contracts;

namespace RateLimiter.Reader.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddSingleton<IReaderService, ReaderService>();
        collection.AddHostedService<HostedService>();

        return collection;
    }
}
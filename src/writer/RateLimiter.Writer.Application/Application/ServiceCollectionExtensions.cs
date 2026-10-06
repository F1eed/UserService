using Microsoft.Extensions.DependencyInjection;
using RateLimiter.Writer.Application.Contracts;

namespace RateLimiter.Writer.Application.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddSingleton<IWriterService, WriterService>();

        return collection;
    }
}
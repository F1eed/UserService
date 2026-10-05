using Microsoft.Extensions.DependencyInjection;

namespace RateLimiter.Writer.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationGrpc(this IServiceCollection collection)
    {
        collection.AddGrpc();
        collection.AddGrpcReflection();
        return collection;
    }
}
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RateLimiter.Writer.Presentation.Interceptors;
using RateLimiter.Writer.Presentation.Validation;

namespace RateLimiter.Writer.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationGrpc(this IServiceCollection collection)
    {
        collection.AddValidatorsFromAssemblyContaining<IAssemblyMarker>(ServiceLifetime.Singleton);

        collection.AddGrpc(options =>
        {
            options.Interceptors.Add<ErrorInterceptor>();
            options.Interceptors.Add<ValidateInterceptor>();
        });

        collection.AddGrpcReflection();
        return collection;
    }
}
using FluentValidation;
using Itmo.HighLoad.Presentation.Grpc.Interceptors;
using Itmo.HighLoad.Presentation.Grpc.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.HighLoad.Presentation.Grpc.Extensions;

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
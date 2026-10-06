using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UserService.Presentation.Grpc.Interceptors;
using UserService.Presentation.Grpc.Validation;

namespace UserService.Presentation.Grpc.Extensions;

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
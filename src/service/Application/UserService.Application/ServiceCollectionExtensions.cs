using Itmo.HighLoad.Application.Contracts.Users;
using Itmo.HighLoad.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.HighLoad.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddSingleton<IUserService, UserService>();

        return collection;
    }
}
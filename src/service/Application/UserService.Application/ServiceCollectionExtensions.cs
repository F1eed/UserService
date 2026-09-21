using Itmo.HighLoad.Application.Contracts.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.HighLoad.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IUserService, Itmo.HighLoad.Application.Users.UserService>();

        return collection;
    }
}
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Contracts.Users;

namespace UserService.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IUserService, Users.UserService>();

        return collection;
    }
}
using Itmo.HighLoad.Application.Contracts.Users.Operations;

namespace Itmo.HighLoad.Application.Contracts.Users;

public interface IUserService
{
    Task<CreateUsers.Result> CreateUsersAsync(
        CreateUsers.Request request,
        CancellationToken cancellationToken);

    Task<GetUser.Result> GetUserAsync(
        GetUser.Request request,
        CancellationToken cancellationToken);

    Task<GetUsers.Result> GetUsersAsync(
        GetUsers.Request request,
        CancellationToken cancellationToken);

    Task<UpdateUser.Result> UpdateUserAsync(
        UpdateUser.Request request,
        CancellationToken cancellationToken);

    Task DeleteUserAsync(
        DeleteUser.Request request,
        CancellationToken cancellationToken);
}
using UserService.Application.Contracts.Users.Operations;

namespace UserService.Application.Contracts.Users;

public interface IUserService
{
    Task CreateUsersAsync(
        CreateUsers.Request request,
        CancellationToken cancellationToken);

    Task<GetUser.Result> GetUserAsync(
        GetUser.Request request,
        CancellationToken cancellationToken);

    Task<GetUsers.Result> GetUsersAsync(
        GetUsers.Request request,
        CancellationToken cancellationToken);

    Task UpdateUserResultAsync(
        UpdateUser.Request request,
        CancellationToken cancellationToken);

    Task DeleteUserAsync(
        DeleteUser.Request request,
        CancellationToken cancellationToken);
}
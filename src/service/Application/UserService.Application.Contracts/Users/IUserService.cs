using UserService.Application.Contracts.Users.Operations;
using UserService.Application.Model.Users;

namespace UserService.Application.Contracts.Users;

public interface IUserService
{
    Task<IReadOnlyCollection<User>> CreateUsersAsync(
        CreateUser.Request request,
        CancellationToken cancellationToken);

    Task<GetUserById.Result> GetUserByIdAsync(
        GetUserById.Request request,
        CancellationToken cancellationToken);

    Task<GetUserByName.Result> GetUserByNameAsync(
        GetUserByName.Request request,
        CancellationToken cancellationToken);

    Task UpdateUserResultAsync(
        UpdateUser.Request request,
        CancellationToken cancellationToken);

    Task DeleteUserAsync(
        DeleteUser.Request request,
        CancellationToken cancellationToken);
}
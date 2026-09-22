using Itmo.HighLoad.Application.Contracts.Users.Operations;

namespace Itmo.HighLoad.Application.Contracts.Users;

public interface IUserService
{
    Task<CreateUsers.Result> CreateUsersAsync(
        CreateUsers.Request createUsersRequest,
        CancellationToken cancellationToken);

    Task<GetUser.Result> GetUserAsync(
        GetUser.Request getUserRequest,
        CancellationToken cancellationToken);

    Task<GetUsers.Result> GetUsersAsync(
        GetUsers.Request getUsersRequest,
        CancellationToken cancellationToken);

    Task<UpdateUser.Result> UpdateUserAsync(
        UpdateUser.Request updateUserRequest,
        CancellationToken cancellationToken);

    Task DeleteUserAsync(
        DeleteUser.Request deleteUserRequest,
        CancellationToken cancellationToken);
}
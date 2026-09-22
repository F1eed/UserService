using Itmo.HighLoad.Application.Abstractions.Persistence.Queries;
using Itmo.HighLoad.Application.Abstractions.Persistence.Repositories;
using Itmo.HighLoad.Application.Contracts.Users;
using Itmo.HighLoad.Application.Contracts.Users.Operations;
using Itmo.HighLoad.Application.Mapping;
using Itmo.HighLoad.Application.Model.Users;

namespace Itmo.HighLoad.Application.Users;

internal class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<CreateUsers.Result> CreateUsersAsync(
        CreateUsers.Request createUsersRequest,
        CancellationToken cancellationToken)
    {
        var users = createUsersRequest.Users
            .Select(x => User.CreateFromService(
                x.Login,
                x.Password,
                x.Name,
                x.Surname,
                x.DateOfBirth))
            .ToList();

        List<User> result = await userRepository
            .AddUsersAsync(users, cancellationToken)
            .ToListAsync(cancellationToken: cancellationToken);

        return new CreateUsers.Result.Success([.. result.Select(x => x.MapToDto())]);
    }

    public async Task<GetUser.Result> GetUserAsync(
        GetUser.Request getUserRequest,
        CancellationToken cancellationToken)
    {
        long query = getUserRequest.Id;

        User? user = await userRepository.QueryByIdAsync(query, cancellationToken);

        if (user is null)
        {
            return new GetUser.Result.Failure("User not found");
        }

        return new GetUser.Result.Success(user.MapToDto());
    }

    public async Task<GetUsers.Result> GetUsersAsync(
        GetUsers.Request getUsersRequest,
        CancellationToken cancellationToken)
    {
        var query = UserQueryByNameAndSurname.Build(builder => builder.WithName(getUsersRequest.Name)
            .WithSurname(getUsersRequest.Surname));

        List<User> users = await userRepository
            .QueryByNameAndSurnameAsync(query, cancellationToken)
            .ToListAsync(cancellationToken: cancellationToken);

        if (users.Count == 0)
        {
            return new GetUsers.Result.Failure("Users not found.");
        }

        return new GetUsers.Result.Success([.. users.Select(user => user.MapToDto())]);
    }

    public async Task<UpdateUser.Result> UpdateUserAsync(
        UpdateUser.Request updateUserRequest,
        CancellationToken cancellationToken)
    {
        var query = UserUpdateQuery.Build(builder => builder
            .WithId(updateUserRequest.Id)
            .WithPassword(updateUserRequest.Password)
            .WithName(updateUserRequest.Name)
            .WithSurname(updateUserRequest.Surname)
            .WithDateOfBirth(updateUserRequest.DateOfBirth));

        if (await userRepository.UpdateUserAsync(query, cancellationToken) is false)
        {
            return new UpdateUser.Result.NotFound();
        }

        return new UpdateUser.Result.Success("User updated successfully");
    }

    public async Task DeleteUserAsync(
        DeleteUser.Request deleteUserRequest,
        CancellationToken cancellationToken)
    {
        long query = deleteUserRequest.Id;

        await userRepository.DeleteUserAsync(query, cancellationToken);
    }
}

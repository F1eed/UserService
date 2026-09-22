using Itmo.HighLoad.Application.Abstractions.Persistence.Queries;
using Itmo.HighLoad.Application.Abstractions.Persistence.Repositories;
using Itmo.HighLoad.Application.Abstractions.Persistence.Transactions;
using Itmo.HighLoad.Application.Contracts.Users;
using Itmo.HighLoad.Application.Contracts.Users.Operations;
using Itmo.HighLoad.Application.Mapping;
using Itmo.HighLoad.Application.Model.Users;
using System.Data;

namespace Itmo.HighLoad.Application.Users;

internal class UserService : IUserService
{
    private readonly IPersistenceTransactionProvider _transactionProvider;
    private readonly IUserRepository _userRepository;

    public UserService(
        IPersistenceTransactionProvider transactionProvider,
        IUserRepository userRepository)
    {
        _transactionProvider = transactionProvider;
        _userRepository = userRepository;
    }

    public async Task<CreateUsers.Result> CreateUsersAsync(
        CreateUsers.Request createUsersRequest,
        CancellationToken cancellationToken)
    {
        await using IPersistenceTransaction transaction = await _transactionProvider
            .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        var users = createUsersRequest.Users
            .Select(x => User.CreateFromService(
                x.Login,
                x.Password,
                x.Name,
                x.Surname,
                x.DateOfBirth))
            .ToList();

        List<User> result = await _userRepository
            .AddUsersAsync(users, cancellationToken)
            .ToListAsync(cancellationToken: cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return new CreateUsers.Result.Success([.. result.Select(x => x.MapToDto())]);
    }

    public async Task<GetUser.Result> GetUserAsync(
        GetUser.Request getUserRequest,
        CancellationToken cancellationToken)
    {
        await using IPersistenceTransaction transaction = await _transactionProvider
            .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        var query = UserQueryById.Build(builder => builder.WithId(getUserRequest.Id));

        User? user = await _userRepository.QueryByIdAsync(query, cancellationToken);

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
        await using IPersistenceTransaction transaction = await _transactionProvider
            .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        var query = UserQueryByNameAndSurname.Build(builder => builder.WithName(getUsersRequest.Name)
            .WithSurname(getUsersRequest.Surname));

        List<User> users = await _userRepository
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
        await using IPersistenceTransaction transaction = await _transactionProvider
            .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        var query = UserUpdateQuery.Build(builder => builder
            .WithId(updateUserRequest.Id)
            .WithPassword(updateUserRequest.Password)
            .WithName(updateUserRequest.Name)
            .WithSurname(updateUserRequest.Surname)
            .WithDateOfBirth(updateUserRequest.DateOfBirth));

        if (await _userRepository.UpdateUserAsync(query, cancellationToken) is false)
        {
            return new UpdateUser.Result.NotFound();
        }

        await transaction.CommitAsync(cancellationToken);

        return new UpdateUser.Result.Success("User updated successfully");
    }

    public async Task DeleteUserAsync(
        DeleteUser.Request deleteUserRequest,
        CancellationToken cancellationToken)
    {
        await using IPersistenceTransaction transaction = await _transactionProvider
            .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        var query = UserDeleteQuery.Build(builder => builder
            .WithId(deleteUserRequest.Id));

        await _userRepository.DeleteUserAsync(query, cancellationToken);

        await transaction.CommitAsync(cancellationToken);
    }
}

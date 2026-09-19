using System.Data;
using UserService.Application.Abstractions.Persistence;
using UserService.Application.Abstractions.Persistence.Queries;
using UserService.Application.Abstractions.Persistence.Transactions;
using UserService.Application.Contracts.Users;
using UserService.Application.Contracts.Users.Operations;
using UserService.Application.Mapping;
using UserService.Application.Model.Users;

namespace UserService.Application.Users;

internal class UserService : IUserService
{
    private readonly IPersistenceTransactionProvider _transactionProvider;
    private readonly IPersistenceContext _context;

    public UserService(IPersistenceTransactionProvider transactionProvider, IPersistenceContext context)
    {
        _transactionProvider = transactionProvider;
        _context = context;
    }

    public async Task CreateUsersAsync(
        CreateUsers.Request request,
        CancellationToken cancellationToken)
    {
        await using IPersistenceTransaction transaction = await _transactionProvider
            .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        var users = request.Users
            .Select(x => User.CreateFromService(
                x.Login,
                x.Password,
                x.Name,
                x.Surname,
                x.DateOfBirth))
            .ToList();

        await _context.UserRepository.AddUsersAsync(
                users,
                cancellationToken)
            .ToListAsync(cancellationToken: cancellationToken);

        await transaction.CommitAsync(cancellationToken);
    }

    public async Task<GetUser.Result> GetUserAsync(
        GetUser.Request request,
        CancellationToken cancellationToken)
    {
        await using IPersistenceTransaction transaction = await _transactionProvider
            .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        var query = UserQueryById.Build(builder => builder
            .WithId(request.Id));

        User? user = await _context.UserRepository.QueryByIdAsync(
            query, cancellationToken);

        if (user is null)
        {
            return new GetUser.Result.Failure("User not found");
        }

        return new GetUser.Result.Success(user.MapToDto());
    }

    public async Task<GetUsers.Result> GetUsersAsync(
        GetUsers.Request request,
        CancellationToken cancellationToken)
    {
        await using IPersistenceTransaction transaction = await _transactionProvider
            .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        var query = UserQueryByNameAndSurname.Build(builder => builder
            .WithName(request.Name)
            .WithSurname(request.Surname));

        List<User> users = [];

        await foreach (User user in _context.UserRepository.QueryByNameAndSurnameAsync(
                           query,
                           cancellationToken))
        {
            users.Add(user);
        }

        if (users.Count == 0)
        {
            return new GetUsers.Result.Failure("Users not found.");
        }

        return new GetUsers.Result.Success(
            users.Select(user => user.MapToDto()).ToList());
    }

    public async Task UpdateUserResultAsync(
        UpdateUser.Request request,
        CancellationToken cancellationToken)
    {
        await using IPersistenceTransaction transaction = await _transactionProvider
            .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        var query = UserUpdateQuery.Build(builder => builder
            .WithId(request.Id)
            .WithPassword(request.Password)
            .WithName(request.Name)
            .WithSurname(request.Surname)
            .WithDateOfBirth(request.DateOfBirth));

        await _context.UserRepository.UpdateUserAsync(
            query,
            cancellationToken);

        await transaction.CommitAsync(cancellationToken);
    }

    public async Task DeleteUserAsync(
        DeleteUser.Request request,
        CancellationToken cancellationToken)
    {
        await using IPersistenceTransaction transaction = await _transactionProvider
            .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        var query = UserDeleteQuery.Build(builder => builder
            .WithId(request.Id));

        await _context.UserRepository.DeleteUserAsync(
            query,
            cancellationToken);

        await transaction.CommitAsync(cancellationToken);
    }
}
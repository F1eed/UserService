using System.Data;
using UserService.Application.Abstractions.Persistence;
using UserService.Application.Abstractions.Persistence.Queries;
using UserService.Application.Abstractions.Persistence.Transactions;
using UserService.Application.Contracts.Users;
using UserService.Application.Contracts.Users.Operations;
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

    public async Task<IReadOnlyCollection<User>> CreateUsersAsync(
        CreateUser.Request request,
        CancellationToken cancellationToken)
    {
        await using IPersistenceTransaction transaction = await _transactionProvider
            .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        var users = await _context.UserRepository.AddUsersAsync(
            request.Users,
            cancellationToken)
            .ToListAsync(cancellationToken: cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return users;
    }

    public async Task<GetUserById.Result> GetUserByIdAsync(
        GetUserById.Request request,
        CancellationToken cancellationToken)
    {
        await using IPersistenceTransaction transaction = await _transactionProvider
            .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        var query = UserQueryById.Build(builder => builder
            .WithId(request.Id));

        User user = await _context.UserRepository.QueryByIdAsync(
            query, cancellationToken);

        return new GetUserById.Result.Success(user);
    }

    public async Task<GetUserByName.Result> GetUserByNameAsync(
        GetUserByName.Request request,
        CancellationToken cancellationToken)
    {
        await using IPersistenceTransaction transaction = await _transactionProvider
            .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        var query = UserQueryByNameAndSurname.Build(builder => builder
            .WithName(request.Name)
            .WithSurname(request.Surname));

        IAsyncEnumerable<User> user = _context.UserRepository.QueryByNameAndSurnameAsync(
            query,
            cancellationToken);

        return new GetUserByName.Result.Success(user);
    }

    public async Task UpdateUserResultAsync(
        UpdateUser.Request request,
        CancellationToken cancellationToken)
    {
        await using IPersistenceTransaction transaction = await _transactionProvider
            .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        var query = UserUpdateQuery.Build(builder => builder
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
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

    public async Task CreateUserAsync(
        CreateUser.Request request,
        CancellationToken cancellationToken)
    {
        await using IPersistenceTransaction transaction = await _transactionProvider
            .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        await _context.UserRepository.AddUserAsync(
            request.Users,
            cancellationToken);

        await transaction.CommitAsync(cancellationToken);
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

    public Task<GetUserByName.Result> GetUserByNameAsync(
        GetUserByName.Request request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateUser.Result> UpdateUserResultAsync(
        UpdateUser.Request request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<DeleteUser.Result> DeleteUserAsync(
        DeleteUser.Request request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
using Npgsql;
using System.Runtime.CompilerServices;
using UserService.Application.Abstractions.Persistence.Queries;
using UserService.Application.Abstractions.Persistence.Repositories;
using UserService.Application.Model.Users;

namespace UserService.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public UserRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async IAsyncEnumerable<User> AddUsersAsync(
        IReadOnlyCollection<User> users,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> QueryByIdAsync(UserQueryById query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<User> QueryByNameAndSurnameAsync(
        UserQueryByNameAndSurname query,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(UserUpdateQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(UserDeleteQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
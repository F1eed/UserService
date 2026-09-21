using Itmo.HighLoad.Application.Abstractions.Persistence.Queries;
using Itmo.HighLoad.Application.Model.Users;
using UserDeleteQuery = Itmo.HighLoad.Application.Abstractions.Persistence.Queries.UserDeleteQuery;

namespace Itmo.HighLoad.Application.Abstractions.Persistence.Repositories;

public interface IUserRepository
{
    IAsyncEnumerable<User> AddUsersAsync(
        IReadOnlyCollection<User> users,
        CancellationToken cancellationToken);

    Task<User?> QueryByIdAsync(
        UserQueryById query,
        CancellationToken cancellationToken);

    IAsyncEnumerable<User> QueryByNameAndSurnameAsync(
        UserQueryByNameAndSurname query,
        CancellationToken cancellationToken);

    Task<bool> UpdateUserAsync(
        UserUpdateQuery query,
        CancellationToken cancellationToken);

    Task DeleteUserAsync(
        UserDeleteQuery query,
        CancellationToken cancellationToken);
}
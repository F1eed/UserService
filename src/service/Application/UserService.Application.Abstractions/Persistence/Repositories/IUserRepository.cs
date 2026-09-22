using Itmo.HighLoad.Application.Abstractions.Persistence.Queries;
using Itmo.HighLoad.Application.Model.Users;

namespace Itmo.HighLoad.Application.Abstractions.Persistence.Repositories;

public interface IUserRepository
{
    IAsyncEnumerable<User> AddUsersAsync(
        IReadOnlyCollection<User> users,
        CancellationToken cancellationToken);

    Task<User?> QueryByIdAsync(
        long userId,
        CancellationToken cancellationToken);

    IAsyncEnumerable<User> QueryByNameAndSurnameAsync(
        UserQueryByNameAndSurname query,
        CancellationToken cancellationToken);

    Task<bool> UpdateUserAsync(
        UserUpdateQuery query,
        CancellationToken cancellationToken);

    Task DeleteUserAsync(
        long userId,
        CancellationToken cancellationToken);
}
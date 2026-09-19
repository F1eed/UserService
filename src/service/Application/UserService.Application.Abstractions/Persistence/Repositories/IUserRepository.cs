using UserService.Application.Abstractions.Persistence.Queries;
using UserService.Application.Model.Users;

namespace UserService.Application.Abstractions.Persistence.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(
        IReadOnlyCollection<User> users,
        CancellationToken cancellationToken);

    Task<User> QueryByIdAsync(
        UserQueryById query,
        CancellationToken cancellationToken);

    IAsyncEnumerable<User> QueryByNameAndSurnameAsync(
        UserQueryByNameAndSurname query,
        CancellationToken cancellationToken);

    Task UpdateUserAsync(
        UserUpdateQuery query,
        CancellationToken cancellationToken);

    Task DeleteUserAsync(
        UserDeleteQuery query,
        CancellationToken cancellationToken);
}
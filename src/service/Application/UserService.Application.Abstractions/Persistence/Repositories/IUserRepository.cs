using UserService.Application.Abstractions.Persistence.Models;
using UserService.Application.Abstractions.Persistence.Queries;
using UserService.Application.Model.Users;

namespace UserService.Application.Abstractions.Persistence.Repositories;

/*TODO:
    * Контракты методов должны возвращать доменную сущность
    * На стороне репозитория должны быть реализованы мапперы из UserDao -> User
    * На самом сервисе не должно быть никакого маппинга
*/
public interface IUserRepository
{
    Task AddUserAsync(
        IReadOnlyCollection<User> users,
        CancellationToken cancellationToken);

    Task<UserDao> QueryByIdAsync(
        UserQueryById query,
        CancellationToken cancellationToken);

    IAsyncEnumerable<UserDao> QueryByNameAndSurnameAsync(
        UserQueryByNameAndSurname query,
        CancellationToken cancellationToken);

    Task UpdateUserAsync(
        UserUpdateQuery query,
        CancellationToken cancellationToken);

    Task DeleteUserAsync(
        UserDeleteQuery query,
        CancellationToken cancellationToken);
}
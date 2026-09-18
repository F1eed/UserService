using UserService.Application.Abstractions.Persistence.Repositories;

namespace UserService.Application.Abstractions.Persistence;

public interface IPersistenceContext
{
    IUserRepository UserRepository { get; }
}
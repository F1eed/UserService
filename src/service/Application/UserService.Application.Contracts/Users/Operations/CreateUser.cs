using UserService.Application.Model.Users;

namespace UserService.Application.Contracts.Users.Operations;

public static class CreateUser
{
    public readonly record struct Request(IReadOnlyCollection<User> Users);
}
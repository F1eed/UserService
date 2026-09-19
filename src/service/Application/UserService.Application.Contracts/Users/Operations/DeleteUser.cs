namespace UserService.Application.Contracts.Users.Operations;

public static class DeleteUser
{
    public readonly record struct Request(Guid Id);
}
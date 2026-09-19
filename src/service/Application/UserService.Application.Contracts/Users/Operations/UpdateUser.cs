namespace UserService.Application.Contracts.Users.Operations;

public static class UpdateUser
{
    public readonly record struct Request(
        string Password,
        string Name,
        string Surname,
        DateTime DateOfBirth);
}
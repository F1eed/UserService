namespace UserService.Application.Contracts.Users.Operations;

public static class CreateUsers
{
    public readonly record struct Request(IReadOnlyCollection<UserEntry> Users);

    public record UserEntry(string Login, string Password, string Name, string Surname, DateTime DateOfBirth);
}
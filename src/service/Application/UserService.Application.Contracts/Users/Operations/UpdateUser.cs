namespace UserService.Application.Contracts.Users.Operations;

public static class UpdateUser
{
    public readonly record struct Request(
        long Id,
        string? Password,
        string? Name,
        string? Surname,
        DateOnly? DateOfBirth);
}
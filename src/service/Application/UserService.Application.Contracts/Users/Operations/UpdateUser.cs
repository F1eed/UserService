namespace UserService.Application.Contracts.Users.Operations;

// TODO: можно сделать поля nullable, не всегда хотим обновить всё
public static class UpdateUser
{
    public readonly record struct Request(
        string Password,
        string Name,
        string Surname,
        DateTime DateOfBirth);
}
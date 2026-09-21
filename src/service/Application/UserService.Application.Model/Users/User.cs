using UserService.Application.Model.Users.ValueObjects;

namespace UserService.Application.Model.Users;

public sealed class User
{
    private User(
        long userId,
        string login,
        string password,
        string name,
        string surname,
        DateOnly? dateOfBirth)
    {
        UserId = new UserId(userId);
        Login = new Login(login);
        Password = new Password(password);
        Name = name;
        Surname = surname;
        DateOfBirth = dateOfBirth is null ? null : new BirthDate(dateOfBirth);
    }

    public static User CreateFromDb(
        long userId,
        string login,
        string password,
        string name,
        string surname,
        DateOnly? dateOfBirth)
    {
        return new User(
            userId,
            login,
            password,
            name,
            surname,
            dateOfBirth);
    }

    public static User CreateFromService(
        string login,
        string password,
        string name,
        string surname,
        DateOnly? dateOfBirth)
    {
        return new User(
            UserId.Default().Value,
            login,
            password,
            name,
            surname,
            dateOfBirth is null ? null : dateOfBirth);
    }

    public UserId UserId { get; }

    public Login Login { get; }

    public Password Password { get; }

    public string Name { get; }

    public string Surname { get; }

    public BirthDate? DateOfBirth { get; }
}

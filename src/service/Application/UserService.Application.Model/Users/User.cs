using UserService.Application.Model.Users.ValueObjects;

namespace UserService.Application.Model.Users;

public sealed class User
{
    private User(
        UserId userId,
        Login login,
        Password password,
        string name,
        string surname,
        DateTime dateOfBirth)
    {
        UserId = userId;
        Login = login;
        Password = password;
        Name = name;
        Surname = surname;
        DateOfBirth = dateOfBirth;
    }

    public static User CreateFromDb(
        UserId userId,
        Login login,
        Password password,
        string name,
        string surname,
        DateTime dateOfBirth)
    {
        return new User(userId, login, password, name, surname, dateOfBirth);
    }

    public static User CreateFromService(
        string login,
        string password,
        string name,
        string surname,
        DateTime dateOfBirth)
    {
        return new User(
            UserId.Default(),
            new Login(login),
            new Password(password),
            name,
            surname,
            dateOfBirth);
    }

    public UserId UserId { get; }

    public Login Login { get; }

    public Password Password { get; }

    public string Name { get; }

    public string Surname { get; }

    public DateTime DateOfBirth { get; }
}

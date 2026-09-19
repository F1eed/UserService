using UserService.Application.Model.Users.ValueObjects;

namespace UserService.Application.Model.Users;

public sealed class User
{
    // TODO: сделать два конструктора, один публичный(который без id), второй приватный(с id)
    // и к нему статический метод типа CreateFromDb i think
    // может даппер будет(но как будто всё равно вызывать такой метод придётся)
    public User(Guid id, Login login, Password password, string name, string surname, DateTime dateOfBirth)
    {
        Id = id;
        Login = login;
        Password = password;
        Name = name;
        Surname = surname;
        DateOfBirth = dateOfBirth;
    }

    public Guid Id { get; }

    public Login Login { get; }

    public Password Password { get; }

    public string Name { get; }

    public string Surname { get; }

    public DateTime DateOfBirth { get; }
}
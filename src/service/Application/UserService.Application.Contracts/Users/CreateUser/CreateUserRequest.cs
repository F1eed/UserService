namespace UserService.Application.Contracts.Users.CreateUser;

public record CreateUserRequest(
    string Login,
    string Password,
    string Name,
    string Surname,
    DateTime Age);
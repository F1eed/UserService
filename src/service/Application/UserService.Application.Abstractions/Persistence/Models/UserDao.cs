namespace UserService.Application.Abstractions.Persistence.Models;

public record UserDao(Guid Id, string Login, string Name, string Surname, DateOnly DateOfBirth);
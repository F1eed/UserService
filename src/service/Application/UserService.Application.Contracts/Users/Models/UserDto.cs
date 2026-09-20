namespace UserService.Application.Contracts.Users.Models;

public record UserDto(string Login, string Name, string Surname, DateOnly DateOfBirth);
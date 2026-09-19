namespace UserService.Application.Contracts.Models;

public record UserDto(string Login, string Name, string Surname, DateTime DateOfBirth);
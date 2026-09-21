namespace Itmo.HighLoad.Application.Contracts.Users.Models;

public record UserDto(long UserId, string Login, string Name, string Surname, DateOnly? DateOfBirth);
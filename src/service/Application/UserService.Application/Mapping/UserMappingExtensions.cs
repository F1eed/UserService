using UserService.Application.Contracts.Users.Models;
using UserService.Application.Model.Users;

namespace UserService.Application.Mapping;

public static class UserMappingExtensions
{
    public static UserDto MapToDto(this User user)
        => new UserDto(user.UserId.Value, user.Login.Value, user.Name, user.Surname, user.DateOfBirth?.Value);
}
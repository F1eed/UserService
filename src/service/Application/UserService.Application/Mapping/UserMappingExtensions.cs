using UserService.Application.Contracts.Models;
using UserService.Application.Model.Users;

namespace UserService.Application.Mapping;

public static class UserMappingExtensions
{
    public static UserDto MapToDto(this User user)
        => new UserDto(user.Login.Value, user.Name, user.Surname, user.DateOfBirth.Value);
}
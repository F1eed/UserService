using Itmo.HighLoad.Application.Contracts.Users.Models;
using Itmo.HighLoad.Application.Model.Users;

namespace Itmo.HighLoad.Application.Mapping;

public static class UserMappingExtensions
{
    public static UserDto MapToDto(this User user)
        => new UserDto(user.UserId.Value, user.Login.Value, user.Name, user.Surname, user.DateOfBirth?.Value);
}
using Itmo.HighLoad.Application.Contracts.Users.Models;
using UserService.Presentation.Grpc;

namespace Itmo.HighLoad.Presentation.Grpc.Mapping;

public static class UserMappingExtensions
{
    public static User MapToProto(this UserDto user)
        => new User
        {
            Id = user.UserId,
            Login = user.Login,
            Name = user.Name,
            Surname = user.Surname,
            BirthDate = user.DateOfBirth?.MapToProto(),
        };
}
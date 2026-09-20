using UserService.Application.Contracts.Models;

namespace UserService.Presentation.Grpc.Mapping;

public static class UserMappingExtensions
{
    public static User MapToProto(this UserDto user)
        => new User
        {
            Login = user.Login,
            Name = user.Name,
            Surname = user.Surname,
            BirthDate = user.DateOfBirth.MapToProto(),
        };
}
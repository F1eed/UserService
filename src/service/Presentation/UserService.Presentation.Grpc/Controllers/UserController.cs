using Grpc.Core;
using UserService.Application.Contracts.Users;

namespace UserService.Presentation.Grpc.Controllers;

public class UserController : UserService.UserServiceBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task<EmptyResponse> CreateUser(
        CreateUserRequest request,
        ServerCallContext context)
    {
        var users = new List<Application.Contracts.Users.Operations.CreateUser.UserEntry>();

        await _userService.CreateUsersAsync(users, context.CancellationToken);
    }
}
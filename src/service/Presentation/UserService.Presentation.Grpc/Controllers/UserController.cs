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

        foreach (CreateUser user in request.User)
        {
            users.Add(new Application.Contracts.Users.Operations.CreateUser.UserEntry(
                user.Login,
                user.Password,
                user.Name,
                user.Surname,
                user.BirthDate.ToDateTime()));
        }

        var serviceRequest = new CreateUser.Request(users);

        await _userService.CreateUsersAsync(, context.CancellationToken);
    }
}
using Grpc.Core;
using UserService.Application.Contracts.Users;
using UserService.Application.Contracts.Users.Operations;

namespace UserService.Presentation.Grpc.Controllers;

public class UserController : UserService.UserServiceBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task<EmptyResponse> CreateUsers(
        CreateUsersRequest request,
        ServerCallContext context)
    {
        var users = request.Entries.Select(user => new CreateUser.UserEntry(
                user.Login,
                user.Password,
                user.Name,
                user.Surname,
                user.BirthDate.ToDateTime()))
            .ToList();

        var serviceRequest = new CreateUser.Request(users);

        await _userService.CreateUsersAsync(serviceRequest, context.CancellationToken);

        return await Task.FromResult(new EmptyResponse());
    }
}
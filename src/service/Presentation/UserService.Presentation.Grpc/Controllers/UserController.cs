using Google.Protobuf.WellKnownTypes;
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

    public override async Task<Empty> CreateUsers(
        CreateUsersRequest request,
        ServerCallContext context)
    {
        var users = request.Entries.Select(user => new CreateUsers.UserEntry(
                user.Login,
                user.Password,
                user.Name,
                user.Surname,
                user.BirthDate.ToDateTime()))
            .ToList();

        var serviceRequest = new CreateUsers.Request(users);

        await _userService.CreateUsersAsync(serviceRequest, context.CancellationToken);

        return await Task.FromResult(new Empty());
    }

    public override async Task<GetUsersResponse> GetUsersByName(
        GetUsersRequest request,
        ServerCallContext context)
    {
        var serviceRequest = new 
    }
}
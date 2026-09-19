using Grpc.Core;
using UserService.Application.Contracts.Users;

namespace UserService.Presentation.Grpc.Users;

public class GrpcUserService : UserService.UserServiceBase
{
    private readonly IUserService _userService;

    public GrpcUserService(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task<EmptyResponse> CreateUser(
        CreateUserRequest request,
        ServerCallContext context)
    {
        await _userService.CreateUsersAsync(, context.CancellationToken);
    }
}
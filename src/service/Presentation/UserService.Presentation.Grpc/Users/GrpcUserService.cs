using UserService.Application.Contracts.Users;

namespace UserService.Presentation.Grpc.Users;

public class GrpcUserService : UserService.UserServiceBase
{
    private readonly IUserService _userService;

    public GrpcUserService(IUserService userService)
    {
        _userService = userService;
    }
}
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Diagnostics;
using UserService.Application.Contracts.Users;
using UserService.Application.Contracts.Users.Operations;
using UserService.Presentation.Grpc.Mapping;

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

    public override async Task<GetUserResponse> GetUser(
        GetUserRequest request,
        ServerCallContext context)
    {
        var serviceRequest = new GetUser.Request(
            request.Id);

        GetUser.Result serviceResult = await _userService.GetUserAsync(
            serviceRequest,
            context.CancellationToken);

        return serviceResult switch
        {
            GetUser.Result.Success success => new GetUserResponse
            {
                User = success.User.MapToProto(),
            },
            GetUser.Result.Failure failure => throw new RpcException(new Status(
                StatusCode.NotFound,
                failure.ErrorMessage)),
            _ => throw new UnreachableException(),
        };
    }

    public override async Task<GetUsersResponse> GetUsers(
        GetUsersRequest request,
        ServerCallContext context)
    {
        var serviceRequest = new GetUsers.Request(
            request.Name,
            request.Surname);

        GetUsers.Result serviceResult = await _userService.GetUsersAsync(
            serviceRequest,
            context.CancellationToken);

        return serviceResult switch
        {
            GetUsers.Result.Success success => new GetUsersResponse
            {
                Users = { success.Users.Select(x => x.MapToProto()) },
            },
            GetUsers.Result.Failure failure => throw new RpcException(new Status(
                StatusCode.NotFound,
                failure.ErrorMessage)),
            _ => throw new UnreachableException(),
        };
    }

    public override Task<Empty> UpdateUser(
        UpdateUserRequest request,
        ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override async Task<Empty> DeleteUser(
        DeleteUserRequest request,
        ServerCallContext context)
    {
        var serviceRequest = new DeleteUser.Request(request.Id);

        await _userService.DeleteUserAsync(serviceRequest, context.CancellationToken);

        return await Task.FromResult(new Empty());
    }
}
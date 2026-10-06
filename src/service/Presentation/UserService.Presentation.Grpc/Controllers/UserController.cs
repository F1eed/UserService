using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Itmo.HighLoad.Application.Contracts.Users;
using Itmo.HighLoad.Application.Contracts.Users.Operations;
using System.Diagnostics;
using UserService.Presentation.Grpc.Mapping;

namespace UserService.Presentation.Grpc.Controllers;

public class UserController : UserService.UserServiceBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task<CreateUsersResponse> CreateUsersController(
        CreateUsersRequest request,
        ServerCallContext context)
    {
        var users = request.Entries.Select(user =>
            {
                DateOnly? birthDate = user.BirthDate is null
                    ? null
                    : new DateOnly(user.BirthDate.Year, user.BirthDate.Month, user.BirthDate.Day);

                return new CreateUsers.UserEntry(
                    user.Login,
                    user.Password,
                    user.Name,
                    user.Surname,
                    birthDate);
            })
            .ToList();

        var serviceRequest = new CreateUsers.Request(users);

        CreateUsers.Result response = await _userService.CreateUsersAsync(
            serviceRequest,
            context.CancellationToken);

        if (response is CreateUsers.Result.Success success)
        {
            return new CreateUsersResponse
            {
                Users =
                {
                    success.Users.Select(x => x.MapToProto()),
                },
            };
        }

        throw new UnreachableException();
    }

    public override async Task<GetUserResponse> GetUserController(
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

    public override async Task<GetUsersResponse> GetUsersController(
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

    public override async Task<UpdateUserResponse> UpdateUserController(
        UpdateUserRequest request,
        ServerCallContext context)
    {
        DateOnly? birthDate = request.BirthDate is null
            ? null
            : new DateOnly(
                request.BirthDate.Year,
                request.BirthDate.Month,
                request.BirthDate.Day);

        var serviceRequest = new UpdateUser.Request(
            request.Id,
            request.HasPassword ? request.Password : null,
            request.HasName ? request.Name : null,
            request.HasSurname ? request.Surname : null,
            birthDate);

        UpdateUser.Result serviceResult = await _userService.UpdateUserAsync(
            serviceRequest,
            context.CancellationToken);

        return serviceResult switch
        {
            UpdateUser.Result.Success success => new UpdateUserResponse
            {
                Reply = success.SuccessMessage,
            },

            UpdateUser.Result.NotFound =>
                throw new RpcException(new Status(StatusCode.NotFound, "User not found")),

            _ => throw new UnreachableException(),
        };
    }

    public override async Task<Empty> DeleteUserController(
        DeleteUserRequest request,
        ServerCallContext context)
    {
        var serviceRequest = new DeleteUser.Request(request.Id);

        await _userService.DeleteUserAsync(
            serviceRequest,
            context.CancellationToken);

        return await Task.FromResult(new Empty());
    }
}

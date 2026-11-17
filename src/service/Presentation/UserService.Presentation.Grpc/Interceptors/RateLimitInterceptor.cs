using Grpc.Core;
using Grpc.Core.Interceptors;
using Itmo.HighLoad.Application.Contracts.Users;

namespace UserService.Presentation.Grpc.Interceptors;

public sealed class RateLimitInterceptor : Interceptor
{
    private readonly IUserBanService _banService;

    public RateLimitInterceptor(IUserBanService banService)
    {
        _banService = banService;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        long userId = (long)context.UserState["user_id"];

        bool isBanned = await _banService.IsBanned(userId, context.CancellationToken);

        if (isBanned)
        {
            throw new RpcException(
                new Status(
                    StatusCode.ResourceExhausted, "User is banned"));
        }

        return await continuation(request, context);
    }
}
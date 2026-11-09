using Grpc.Core;
using Grpc.Core.Interceptors;

namespace UserService.Presentation.Grpc.Interceptors;

public sealed class UserIdInterceptors : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        string? value = context.RequestHeaders
            .FirstOrDefault(x => x.Key == "user_id")
            ?.Value;

        if (long.TryParse(value, out long userId) is false || userId <= 0)
        {
            throw new RpcException(
                new Status(
                    StatusCode.Unauthenticated, "User id is not valid"));
        }

        context.UserState["user_id"] = userId;
        return await continuation(request, context);
    }
}
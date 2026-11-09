using Grpc.Core;
using Grpc.Core.Interceptors;

namespace UserService.Presentation.Grpc.Interceptors;

public sealed class RateLimitInterceptor : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        long userId = (long)context.UserState["user_id"];
        
        ///<summary>
        ///TODO: хочется потыкать RedisServce с каким-нибудь методом IsBanned(userId);
        ///</summary>
    }
}
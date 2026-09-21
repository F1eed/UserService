using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Itmo.HighLoad.Presentation.Grpc.Interceptors;

public sealed partial class ErrorInterceptor : Interceptor
{
    private readonly ILogger _logger;

    public ErrorInterceptor(ILogger logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (RpcException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Logs.LogUnhandledException(_logger, context.Method, ex.Message);
            throw new RpcException(new Status(StatusCode.Unknown, $"Unhandled error: {ex.Message}"));
        }
    }

    internal static partial class Logs
    {
        [LoggerMessage(1,
            LogLevel.Error,
            "Unhandled error in gRPC method {methodName}: {error}")]
        public static partial void LogUnhandledException(
            ILogger logger,
            string methodName,
            string error);
    }
}
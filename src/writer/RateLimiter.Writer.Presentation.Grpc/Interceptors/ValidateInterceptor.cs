using FluentValidation;
using FluentValidation.Results;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RateLimiter.Writer.Presentation.Interceptors;

public sealed partial class ValidateInterceptor : Interceptor
{
    private readonly ILogger<ValidateInterceptor> _logger;

    public ValidateInterceptor(ILogger<ValidateInterceptor> logger)
    {
        _logger = logger;
    }

    public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        IValidator<TRequest>? validator = context
            .GetHttpContext()
            .RequestServices
            .GetService<IValidator<TRequest>>();

        if (validator is not null)
        {
            ValidationResult result = validator.Validate(request);

            if (result.IsValid is false)
            {
                string formattedErrors = string.Join("; ", result.Errors.Select(
                    error => $"'{error.ErrorMessage}'"));

                Logs.LogValidationFailure(_logger, context.Method, formattedErrors);

                throw new RpcException(new Status(
                    StatusCode.InvalidArgument,
                    $"Validation failed: {formattedErrors}"));
            }
        }

        return continuation(request, context);
    }

    internal static partial class Logs
    {
        [LoggerMessage(1,
            LogLevel.Warning,
            "Validation in gRPC failed: {methodName}\n Errors: {errors}")]
        public static partial void LogValidationFailure(ILogger logger, string methodName, string errors);
    }
}

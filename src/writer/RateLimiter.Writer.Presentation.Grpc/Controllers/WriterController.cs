using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using RateLimiter.Writer.Application.Contracts;
using RateLimiter.Writer.Application.Contracts.Operations;
using RateLimiter.Writer.Presentation.Mapping;
using System.Diagnostics;

namespace RateLimiter.Writer.Presentation.Controllers;

public class WriterController : Writer.WriterBase
{
    private readonly IWriterService _writerService;

    public WriterController(IWriterService writerService)
    {
        _writerService = writerService;
    }

    public override async Task<CreateLimitResponse> CreateLimitController(
        CreateLimitRequest request,
        ServerCallContext context)
    {
        var rateLimit = new CreateLimit.RateLimitEntry(
            request.RateLimit.Route,
            request.RateLimit.RequestsPerMinute);

        var serviceRequest = new CreateLimit.Request(rateLimit);

        CreateLimit.Response serviceResult = await _writerService.CreateLimitAsync(
            serviceRequest,
            context.CancellationToken);

        return serviceResult switch
        {
            CreateLimit.Response.Success success => new CreateLimitResponse
            {
                RateLimit = success.RateLimit.MapToProto(),
            },
            _ => throw new UnreachableException(),
        };
    }

    public override async Task<GetLimitResponse> GetLimitByRouteController(
        GetLimitRequest request,
        ServerCallContext context)
    {
        var serviceRequest = new GetLimitByRoute.Request(request.Route);

        GetLimitByRoute.Response serviceResult = await _writerService.GetLimitByRouteAsync(
            serviceRequest,
            context.CancellationToken);

        return serviceResult switch
        {
            GetLimitByRoute.Response.Success success => new GetLimitResponse
            {
                RateLimit = success.RateLimit.MapToProto(),
            },
            GetLimitByRoute.Response.Failure failure => throw new RpcException(new Status(
                StatusCode.NotFound,
                failure.ErrorMessage)),
            _ => throw new UnreachableException(),
        };
    }

    public override async Task<UpdateLimitResponse> UpdateLimitController(
        UpdateLimitRequest request,
        ServerCallContext context)
    {
        var serviceRequest = new UpdateLimit.Request(request.Route, request.RequestsPerMinute);

        UpdateLimit.Response serviceResult = await _writerService.UpdateLimitAsync(
            serviceRequest,
            context.CancellationToken);
        return serviceResult switch
        {
            UpdateLimit.Response.Success success => new UpdateLimitResponse()
            {
                Reply = success.SuccessMessage,
            },
            UpdateLimit.Response.NotFound => throw new RpcException(new Status(
                StatusCode.NotFound,
                "Not found")),
            _ => throw new UnreachableException(),
        };
    }

    public override async Task<Empty> DeleteLimitController(DeleteLimitRequest request, ServerCallContext context)
    {
        var serviceRequest = new DeleteLimit.Request(request.Route);

        await _writerService.DeleteLimitAsync(serviceRequest, context.CancellationToken);

        return await Task.FromResult(new Empty());
    }
}

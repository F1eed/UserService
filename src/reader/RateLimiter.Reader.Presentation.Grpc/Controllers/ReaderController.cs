using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using RateLimiter.Reader.Application.Contracts;
using RateLimiter.Reader.Presentation.Mapping;

namespace RateLimiter.Reader.Presentation.Controllers;

public class ReaderController : Reader.ReaderBase
{
    private readonly IReaderService _readerService;

    public ReaderController(IReaderService readerService)
    {
        _readerService = readerService;
    }

    public override Task<GetAllLimitsResponse> GetAllLimits(
        Empty request,
        ServerCallContext context)
    {
        IEnumerable<Application.Models.RateLimit> limits = _readerService.GetAllLimits();

        var response = new GetAllLimitsResponse
        {
            Limits = { limits.Select(x => x.ToProto()) },
        };

        return Task.FromResult(response);
    }
}
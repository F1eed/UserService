using Grpc.Core;

namespace Itmo.HighLoad.RateLimiter.Reader.Controllers;

public class ExampleController : Reader.ReaderBase
{
    public override Task<PingResponse> Ping(PingRequest request, ServerCallContext context)
    {
        return Task.FromResult(new PingResponse
        {
            Status = "Alive",
        });
    }
}
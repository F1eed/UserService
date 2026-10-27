using Microsoft.AspNetCore.Builder;
using RateLimiter.Reader.Presentation.Controllers;

namespace RateLimiter.Reader.Presentation.Extensions;

public static class WebApplicationExtensions
{
    public static void UsePresentationGrpc(
        this WebApplication app)
    {
        app.MapGrpcService<ReaderController>();
        app.MapGrpcReflectionService();
    }
}
using Microsoft.AspNetCore.Builder;
using RateLimiter.Writer.Presentation.Controllers;

namespace RateLimiter.Writer.Presentation.Extensions;

public static class WebApplicationExtensions
{
    public static void UsePresentationGrpc(this WebApplication app)
    {
        app.MapGrpcService<WriterController>();
        app.MapGrpcReflectionService();
    }
}
using Itmo.HighLoad.Presentation.Grpc.Controllers;
using Microsoft.AspNetCore.Builder;

namespace Itmo.HighLoad.Presentation.Grpc.Extensions;

public static class WebApplicationExtensions
{
    public static void UsePresentationGrpc(this WebApplication app)
    {
        app.MapGrpcService<UserController>();
        app.MapGrpcReflectionService();
    }
}
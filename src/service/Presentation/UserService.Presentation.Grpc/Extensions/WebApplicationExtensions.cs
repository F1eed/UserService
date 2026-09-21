using Itmo.HighLoad.Presentation.Grpc.Controllers;

namespace Itmo.HighLoad.Presentation.Grpc.Extensions;

public static class WebApplicationExtensions
{
    public static void UsePresentationGrpc(this WebApplication app)
    {
        app.MapGrpcService<UserController>();
        app.MapGrpcReflectionService();
    }
}
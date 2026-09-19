using UserService.Presentation.Grpc.Controllers;

namespace UserService.Presentation.Grpc.Extensions;

public static class WebApplicationExtensions
{
    public static void UsePresentationGrpc(this WebApplication app)
    {
        app.MapGrpcService<UserController>();
        app.MapGrpcReflectionService();
    }
}
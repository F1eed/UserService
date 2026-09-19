using UserService.Application;
using UserService.Infrastructure.Persistence;
using UserService.Presentation.Grpc.Extensions;

#pragma warning disable CA1506

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructurePersistence(builder.Configuration)
    .AddPresentationGrpc();

var app = builder.Build();

app.UseRouting();
app.UsePresentationGrpc();

app.Run();
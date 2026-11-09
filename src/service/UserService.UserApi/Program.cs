using FluentMigrator.Runner;
using Itmo.HighLoad.Application;
using Itmo.HighLoad.Infrastructure.Persistence;
using UserService.Infrastructure.UserBanned;
using UserService.Presentation.Grpc.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructurePersistence(builder.Configuration)
    .AddPresentationGrpc()
    .AddRedis(builder.Configuration);

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.UseRouting();
app.UsePresentationGrpc();

await app.RunAsync();
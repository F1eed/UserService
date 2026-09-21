using FluentMigrator.Runner;
using UserService.Application;
using UserService.Infrastructure.Persistence;
using UserService.Presentation.Grpc.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructurePersistence(builder.Configuration)
    .AddPresentationGrpc();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.UseRouting();
app.UsePresentationGrpc();

await app.RunAsync();
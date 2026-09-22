using FluentMigrator.Runner;
using Itmo.HighLoad.Application;
using Itmo.HighLoad.Infrastructure.Persistence;
using Itmo.HighLoad.Presentation.Grpc.Extensions;

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
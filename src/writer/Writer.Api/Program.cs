using RateLimiter.Writer.Application.Application;
using RateLimiter.Writer.Infrastructure;
using RateLimiter.Writer.Presentation.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPresentationGrpc()
    .AddInfrastructurePersistence(builder.Configuration);

WebApplication app = builder.Build();

app.UseRouting();
app.UsePresentationGrpc();

await app.RunAsync();

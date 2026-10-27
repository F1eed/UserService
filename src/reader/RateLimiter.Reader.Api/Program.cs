using RateLimiter.Reader.Application;
using RateLimiter.Reader.Infrastructure;
using RateLimiter.Reader.Presentation.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPersistenceContext()
    .AddPresentationGrpc()
    .AddApplication();

WebApplication app = builder.Build();

app.UseRouting();
app.UsePresentationGrpc();

await app.RunAsync();
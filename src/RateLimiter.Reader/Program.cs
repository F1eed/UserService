using Itmo.HighLoad.RateLimiter.Reader.Controllers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ExampleController>();

await app.RunAsync("http://*:5000");
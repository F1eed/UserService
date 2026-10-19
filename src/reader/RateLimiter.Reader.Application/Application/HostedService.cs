using Microsoft.Extensions.Hosting;
using RateLimiter.Reader.Application.Contracts;

namespace RateLimiter.Reader.Application.Application;

public class HostedService : IHostedService
{
    private readonly IReaderService _readerService;

    public HostedService(IReaderService readerService)
    {
        _readerService = readerService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return _readerService.InitializeAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
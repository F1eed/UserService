using Microsoft.Extensions.Options;
using RateLimiter.Reader.Application.Abstractions.Redis.Repositories;
using RateLimiter.Reader.Application.Contracts;
using RateLimiter.Reader.Application.Models;

namespace RateLimiter.Reader.Application.Application;

public sealed class RedisService : IRedisService
{
    private readonly IRateLimitStatisticsRepository _statisticsRepository;
    private readonly IReaderService _readerService;
    private readonly RateLimitStatisticOptions _options;

    public RedisService(
        IReaderService readerService,
        IRateLimitStatisticsRepository statisticsRepository,
        IOptions<RateLimitStatisticOptions> options)
    {
        _readerService = readerService;
        _statisticsRepository = statisticsRepository;
        _options = options.Value;
    }

    public async Task ProcessRequestAsync(
        long userId,
        string endpoint,
        CancellationToken cancellationToken)
    {
        RateLimit? limit = _readerService
            .GetAllLimits()
            .FirstOrDefault(x => x.Route == endpoint);

        if (limit is null)
        {
            return;
        }

        await _statisticsRepository.ProcessRequestAsync(
            userId,
            limit.Route,
            limit.RequestsPerMinute.Value,
            _options.Window,
            _options.Ban,
            cancellationToken);
    }
}
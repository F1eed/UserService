using RateLimiter.Reader.Application.Abstractions.Redis.Repositories;
using StackExchange.Redis;

namespace RateLimiter.Reader.RateLimitsStatisticsRepository;

public class RateLimitStatisticsRepository : IRateLimitStatisticsRepository
{
    private const string Script =
        """
        local counterKey = KEYS[1]
        local banKey = KEYS[2]
        
        if (redis.call('EXISTS', banKey) == 1) then
            return
        end
        
        local count = tonumber(redis.call('GET', counterKey)) or 0
        
        local RPM = tonumber(ARGV[1])
        local window = tonumber(ARGV[2])
        local ban = tonumber(ARGV[3])
        
        if (count >= RPM) then
            redis.call('SET', banKey, '1', 'EX', ban)
            redis.call('DEL', counterKey)
        else
            local newCount = redis.call('INCR', counterKey)
            if (newCount == 1) then
                redis.call('EXPIRE', counterKey, window)
            end
        end
        """;

    private readonly IConnectionMultiplexer _connection;

    public RateLimitStatisticsRepository(IConnectionMultiplexer connection)
    {
        _connection = connection;
    }

    public async Task ProcessRequestAsync(
        long userId,
        string route,
        int requestsPerMinute,
        TimeSpan window,
        TimeSpan ban,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        IDatabase database = _connection.GetDatabase();

        RedisKey counterKey = $"rpm:{userId}:{route}";
        RedisKey banKey = $"ban:{userId}";

        await database.ScriptEvaluateAsync(
            Script,
            keys: [counterKey, banKey],
            values: [requestsPerMinute, window.TotalSeconds, window.TotalSeconds]);
    }
}
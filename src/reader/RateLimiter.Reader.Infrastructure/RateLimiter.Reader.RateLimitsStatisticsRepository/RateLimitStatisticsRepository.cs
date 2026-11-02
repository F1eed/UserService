using RateLimiter.Reader.Application.Abstractions.Redis.Repositories;
using StackExchange.Redis;

namespace RateLimiter.Reader.RateLimitsStatisticsRepository;

public class RateLimitStatisticsRepository : IRateLimitStatisticsRepository
{
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

        // Можно наверное количество ретраев сделать, но я не знаю как:D
        while (true)
        {
            if (await database.KeyExistsAsync(banKey))
                return;

            RedisValue current = await database.StringGetAsync(counterKey);

            long count = current.IsNull ? 0 : (long)current;

            ITransaction transaction = database.CreateTransaction();

            transaction.AddCondition(Condition.KeyNotExists(banKey));
            transaction.AddCondition(current.IsNull
                ? Condition.KeyNotExists(counterKey)
                : Condition.StringEqual(counterKey, current));

            if (count >= requestsPerMinute)
            {
                _ = transaction.StringSetAsync(
                    banKey,
                    "1",
                    ban,
                    When.Always);

                _ = transaction.KeyDeleteAsync(counterKey);
            }
            else
            {
                _ = transaction.StringIncrementAsync(counterKey);

                if (current.IsNull)
                {
                    _ = transaction.KeyExpireAsync(counterKey, window);
                }
            }

            bool commited = await transaction.ExecuteAsync();

            if (commited)
                return;
        }
    }
}
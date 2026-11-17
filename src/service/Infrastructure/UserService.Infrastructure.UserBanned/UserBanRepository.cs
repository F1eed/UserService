using Itmo.HighLoad.Application.Abstractions;
using StackExchange.Redis;

namespace UserService.Infrastructure.UserBanned;

public class UserBanRepository : IUserBanRepository
{
    private readonly IConnectionMultiplexer _connection;

    public UserBanRepository(IConnectionMultiplexer connection)
    {
        _connection = connection;
    }

    public async Task<bool> IsUserBannedAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        IDatabase database = _connection.GetDatabase();

        RedisKey banKey = $"ban:{userId}";

        return await database.KeyExistsAsync(banKey);
    }
}
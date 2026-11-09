namespace Itmo.HighLoad.Application.Abstractions;

public interface IUserBanRepository
{
    Task<bool> IsUserBannedAsync(
        long userId,
        CancellationToken cancellationToken);
}
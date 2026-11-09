namespace Itmo.HighLoad.Application.Contracts.Users;

public interface IUserBanService
{
    Task<bool> IsBannedAsync(
        long userId,
        CancellationToken cancellationToken);
}
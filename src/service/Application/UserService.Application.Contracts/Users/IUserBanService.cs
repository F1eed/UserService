namespace Itmo.HighLoad.Application.Contracts.Users;

public interface IUserBanService
{
    Task<bool> IsBanned(
        long userId,
        CancellationToken cancellationToken);
}
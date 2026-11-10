using Itmo.HighLoad.Application.Abstractions;
using Itmo.HighLoad.Application.Contracts.Users;

namespace Itmo.HighLoad.Application.Users;

public sealed class UserBanService : IUserBanService
{
    private readonly IUserBanRepository _userBanRepository;

    public UserBanService(IUserBanRepository userBanRepository)
    {
        _userBanRepository = userBanRepository;
    }

    public Task<bool> IsBanned(long userId, CancellationToken cancellationToken)
    {
        return _userBanRepository.IsUserBannedAsync(
            userId,
            cancellationToken);
    }
}
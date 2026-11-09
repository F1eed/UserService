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

    public Task<bool> IsBannedAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        Task<bool> response = _userBanRepository.IsUserBannedAsync(
            userId,
            cancellationToken);

        return response;
    }
}
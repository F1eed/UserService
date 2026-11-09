using Itmo.HighLoad.Application.Model.Users.ValueObjects;

namespace Itmo.HighLoad.Application.Abstractions;

public interface IUserBanRepository
{
    Task<bool> IsUserBannedAsync(UserId userId);
}
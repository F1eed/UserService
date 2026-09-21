using Itmo.HighLoad.Application.Contracts.Users.Models;

namespace Itmo.HighLoad.Application.Contracts.Users.Operations;

public static class GetUser
{
    public record Request(long Id);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success(UserDto User) : Result;

        public sealed record Failure(string ErrorMessage) : Result;
    }
}
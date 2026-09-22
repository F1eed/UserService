using Itmo.HighLoad.Application.Contracts.Users.Models;

namespace Itmo.HighLoad.Application.Contracts.Users.Operations;

public static class GetUsers
{
    public record Request(string Name, string Surname);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success(IReadOnlyCollection<UserDto> Users) : Result;

        public sealed record Failure(string ErrorMessage) : Result;
    }
}
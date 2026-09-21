using Itmo.HighLoad.Application.Contracts.Users.Models;

namespace Itmo.HighLoad.Application.Contracts.Users.Operations;

public static class CreateUsers
{
    public readonly record struct Request(IReadOnlyCollection<UserEntry> Users);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success(IReadOnlyCollection<UserDto> Users) : Result;
    }

    public record UserEntry(string Login, string Password, string Name, string Surname, DateOnly? DateOfBirth);
}
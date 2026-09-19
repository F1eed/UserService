using UserService.Application.Contracts.Models;

namespace UserService.Application.Contracts.Users.Operations;

public static class GetUserByName
{
    public record Request(string Name, string Surname);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success(IReadOnlyCollection<UserDto> Users) : Result;

        public sealed record Failure(string ErrorMessage) : Result;
    }
}
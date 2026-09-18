using UserService.Application.Model.Users;

namespace UserService.Application.Contracts.Users.Operations;

public static class GetUserByName
{
    public record Request(string Name, string Surname);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success(IAsyncEnumerable<User> Users) : Result;

        public sealed record Failure(string ErrorMessage) : Result;
    }
}
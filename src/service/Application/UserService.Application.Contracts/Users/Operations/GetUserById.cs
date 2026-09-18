using UserService.Application.Model.Users;

namespace UserService.Application.Contracts.Users.Operations;

public static class GetUserById
{
    public record Request(Guid Id);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success(User User) : Result;

        public sealed record Failure(string ErrorMessage) : Result;
    }
}
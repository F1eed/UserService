namespace UserService.Application.Contracts.Users.Operations;

public static class DeleteUser
{
    public record Request(Guid Id);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success() : Result;

        public sealed record Failure(string ErrorMessage) : Result;
    }
}
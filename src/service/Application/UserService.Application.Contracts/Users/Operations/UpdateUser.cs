namespace UserService.Application.Contracts.Users.Operations;

public static class UpdateUser
{
    public record Request(
        string Password,
        string Name,
        string Surname,
        DateTime DateOfBirth);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success() : Result;

        public sealed record Failure(string ErrorMessage) : Result;
    }
}
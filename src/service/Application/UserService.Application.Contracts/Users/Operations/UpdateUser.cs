namespace Itmo.HighLoad.Application.Contracts.Users.Operations;

public static class UpdateUser
{
    public readonly record struct Request(
        long Id,
        string? Password,
        string? Name,
        string? Surname,
        DateOnly? DateOfBirth);

    public abstract record Result
    {
        private Result() { }

        public sealed record Success(string SuccessMessage) : Result;

        public sealed record NotFound : Result;
    }
}
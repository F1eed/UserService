namespace UserService.Application.Contracts.Users.CreateUser;

public abstract record CreateUserResult
{
    private CreateUserResult() { }

    public sealed record Success : CreateUserResult;

    public sealed record Failure(string ErrorMessage) : CreateUserResult;
}
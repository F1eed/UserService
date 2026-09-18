namespace UserService.Application.Contracts.Users.CreateUser;

// TODO: сделай один статик класс CreateUser, в него запихни Result(эти строчки) и Request
public abstract record CreateUserResult
{
    private CreateUserResult() { }

    public sealed record Success : CreateUserResult;

    public sealed record Failure(string ErrorMessage) : CreateUserResult;
}
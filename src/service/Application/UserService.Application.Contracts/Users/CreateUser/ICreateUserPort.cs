namespace UserService.Application.Contracts.Users.CreateUser;

public interface ICreateUserPort
{
    void Execute(CreateUserRequest request);
}
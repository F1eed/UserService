namespace UserService.Application.Model.ValueObjects;

public sealed class Login
{
    public Login(string value)
    {
        if (value.Length <= 0)
        {
            throw new ArgumentException("Login must have a length of at least 1");
        }

        Value = value;
    }

    public string Value { get; private set; }
}
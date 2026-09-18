namespace UserService.Application.Model.ValueObjects;

public sealed class Login
{
    public Login(string value)
    {
        if (value.Length <= 2)
        {
            throw new ArgumentException("Login must have a length of at least 3");
        }

        Value = value;
    }

    public string Value { get; private set; }
}
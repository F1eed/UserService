namespace UserService.Application.Model.ValueObjects;

public sealed class Password
{
    public Password(string value)
    {
        if (value.Length <= 0)
        {
            throw new ArgumentException("Password must have a length of at least 1");
        }

        Value = value;
    }

    public string Value { get; private set; }
}
namespace UserService.Application.Model.Users.ValueObjects;

public class UserId
{
    public UserId(long value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value);
    }

    public static UserId Default()
        => new UserId(0);

    public long Value { get; private set; }
}
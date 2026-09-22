namespace Itmo.HighLoad.Application.Model.Users.ValueObjects;

public readonly record struct UserId
{
    public UserId(long value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value);

        Value = value;
    }

    public static UserId Default()
        => new UserId(0);

    public long Value { get; }
}
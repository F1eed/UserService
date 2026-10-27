namespace Itmo.HighLoad.UserRequestsKafkaGenerator.Models;

public readonly record struct UserId
{
    public UserId(int value)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
        Value = value;
    }

    public int Value { get; }
}
namespace RateLimiter.Reader.Application.Models.ValueObjects;

public readonly record struct RequestsPerMinute
{
    public int Value { get; }

    public RequestsPerMinute(int value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value);

        Value = value;
    }
}

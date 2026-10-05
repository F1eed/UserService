namespace Itmo.HighLoad.RateLimiter.Writer.RateLimit.ValueObjects;

public readonly record struct RequestsPerMinute
{
    public int Value { get; }

    private RequestsPerMinute(int value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value);

        Value = value;
    }

    public static RequestsPerMinute Create(int value)
    {
        return new RequestsPerMinute(value);
    }
}
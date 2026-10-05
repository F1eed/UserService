namespace RateLimiter.Writer.Application.Models.ValueObjects;

public readonly record struct RequestsPerMinute
{
    public long Value { get; }

    private RequestsPerMinute(long value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value);

        Value = value;
    }

    public static RequestsPerMinute Create(long value)
    {
        return new RequestsPerMinute(value);
    }
}
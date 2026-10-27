namespace Itmo.HighLoad.UserRequestsKafkaGenerator.Models;

public readonly record struct Endpoint
{
    public Endpoint(string value)
    {
        value = value.Trim();
        ArgumentException.ThrowIfNullOrEmpty(value);
        Value = value;
    }

    public string Value { get; }
}
namespace RateLimiter.Reader.KafkaConsumer.Options;

public class KafkaOptions
{
    public required string BootstrapServers { get; init; }

    public required string Topic { get; init; }

    public required string GroupId { get; init; }
}
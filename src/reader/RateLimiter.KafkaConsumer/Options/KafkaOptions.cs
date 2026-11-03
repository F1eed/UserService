using Confluent.Kafka;

namespace RateLimiter.Reader.KafkaConsumer.Options;

public class KafkaOptions
{
    public required string BootstrapServers { get; init; }

    public required string Topic { get; init; }

    public required string GroupId { get; init; }

    public AutoOffsetReset AutoOffsetReset { get; init; } = AutoOffsetReset.Earliest;

    public bool EnableAutoCommit { get; init; } = false;
}
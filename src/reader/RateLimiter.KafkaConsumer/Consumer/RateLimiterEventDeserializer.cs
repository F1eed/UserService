using Confluent.Kafka;
using System.Text.Json;

namespace RateLimiter.Reader.KafkaConsumer.Consumer;

public sealed class RateLimiterEventDeserializer<TMessage> : IDeserializer<TMessage>
{
    public TMessage Deserialize(
        ReadOnlySpan<byte> data,
        bool isNull,
        SerializationContext context)
    {
        if (isNull)
        {
            throw new JsonException();
        }

        TMessage? message = JsonSerializer.Deserialize<TMessage>(data);

        return message ?? throw new JsonException();
    }
}
using Confluent.Kafka;
using System.Text.Json;

namespace Itmo.HighLoad.UserRequestsKafkaGenerator.Producer;

public class RateLimiterEventSerializer<TMessage> : ISerializer<TMessage>
{
    public byte[] Serialize(TMessage data, SerializationContext context)
    {
        return JsonSerializer.SerializeToUtf8Bytes(data);
    }
}
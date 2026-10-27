using Confluent.Kafka;
using Itmo.HighLoad.UserRequestsKafkaGenerator.Options;
using Microsoft.Extensions.Options;

namespace Itmo.HighLoad.UserRequestsKafkaGenerator.Producer;

public class KafkaProducer<TMessage> : IKafkaProducer<TMessage>
{
    private readonly IProducer<string, TMessage> _producer;
    private readonly string _topic;

    public KafkaProducer(IOptions<KafkaOptions> options)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = options.Value.BootstrapService,
        };

        _producer = new ProducerBuilder<string, TMessage>(config)
            .SetValueSerializer(new RateLimiterEventSerializer<TMessage>())
            .Build();

        _topic = options.Value.Topic;
    }

    public async Task ProduceAsync(int key, TMessage message, CancellationToken cancellationToken)
    {
        await _producer.ProduceAsync(
            _topic,
            new Message<string, TMessage>
            {
                Key = key.ToString(),
                Value = message,
            },
            cancellationToken);
    }

    public void Dispose()
    {
        _producer.Dispose();
    }
}
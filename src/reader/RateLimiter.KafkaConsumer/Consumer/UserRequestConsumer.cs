using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RateLimiter.Reader.Application.Contracts;
using RateLimiter.Reader.KafkaConsumer.Models;
using RateLimiter.Reader.KafkaConsumer.Options;

namespace RateLimiter.Reader.KafkaConsumer.Consumer;

public sealed class UserRequestConsumer(
    IOptions<KafkaOptions> options,
    IRedisService service) : BackgroundService
{
    private readonly KafkaOptions _options = options.Value;

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _options.BootstrapServers,
            GroupId = _options.GroupId,
            AutoOffsetReset = _options.AutoOffsetReset,
            EnableAutoCommit = _options.EnableAutoCommit,
        };

        using IConsumer<Ignore, RateLimiterKafkaEvent> consumer =
            new ConsumerBuilder<Ignore, RateLimiterKafkaEvent>(config)
                .SetValueDeserializer(
                    new RateLimiterEventDeserializer<RateLimiterKafkaEvent>())
                .Build();

        consumer.Subscribe(_options.Topic);

        try
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                ConsumeResult<Ignore, RateLimiterKafkaEvent> consumeResult;

                try
                {
                    consumeResult = consumer.Consume(stoppingToken);
                }
                catch (ConsumeException e)
                    when (e.Error.Code == ErrorCode.Local_ValueDeserialization)
                {
                    continue;
                }

                RateLimiterKafkaEvent request = consumeResult.Message.Value;

                await service.ProcessRequestAsync(
                    request.UserId,
                    request.Endpoint,
                    stoppingToken);

                consumer.Commit(consumeResult);
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
        }
        finally
        {
            consumer.Close();
        }
    }
}
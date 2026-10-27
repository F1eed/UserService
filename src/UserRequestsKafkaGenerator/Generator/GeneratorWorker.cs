using Confluent.Kafka;
using Itmo.HighLoad.UserRequestsKafkaGenerator.Models;
using Itmo.HighLoad.UserRequestsKafkaGenerator.Producer;

namespace Itmo.HighLoad.UserRequestsKafkaGenerator.Generator;

public class GeneratorWorker : IAsyncDisposable
{
    private readonly IKafkaProducer<RateLimiterKafkaEvent> _producer;
    private readonly CancellationTokenSource _cts;
    private Task? _task;

    public GeneratorConfig Config { get; }

    public GeneratorWorker(
        GeneratorConfig config,
        IKafkaProducer<RateLimiterKafkaEvent> producer)
    {
        Config = config;
        _producer = producer;
        _cts = new CancellationTokenSource();
    }

    public void Work()
    {
        if (_task is not null)
        {
            return;
        }

        _task = ExecuteAsync(_cts.Token);
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            await _cts.CancelAsync();

            if (_task is not null)
            {
                await _task;
            }
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            _cts.Dispose();
        }
    }

    private async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var timer = new PeriodicTimer(
            TimeSpan.FromMinutes(1.0 / Config.RequestsPerMinute));

        while (await timer.WaitForNextTickAsync(cancellationToken))
        {
            try
            {
                await _producer.ProduceAsync(
                    Config.UserId.Value,
                    new RateLimiterKafkaEvent(Config.UserId, Config.Endpoint),
                    cancellationToken);
            }
            catch (ProduceException<string, RateLimiterKafkaEvent> e)
            {
                await Console.Error.WriteLineAsync($"Kafka error: {e.Error.Reason}");
            }
        }
    }
}
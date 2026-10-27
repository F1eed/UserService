using Itmo.HighLoad.UserRequestsKafkaGenerator.Models;
using Itmo.HighLoad.UserRequestsKafkaGenerator.Producer;
using System.Collections.Concurrent;

namespace Itmo.HighLoad.UserRequestsKafkaGenerator.Generator;

public class GeneratorManager : IAsyncDisposable
{
    private readonly IKafkaProducer<RateLimiterKafkaEvent> _producer;
    private readonly ConcurrentDictionary<Guid, GeneratorWorker> _workers = new();

    public GeneratorManager(IKafkaProducer<RateLimiterKafkaEvent> producer)
    {
        _producer = producer;
    }

    public Guid AddWorker(
        int userId,
        string endpoint,
        int requestsPerMinute)
    {
        var guid = Guid.NewGuid();

        var config = new GeneratorConfig(
            guid,
            new UserId(userId),
            new Endpoint(endpoint),
            requestsPerMinute);

        _workers[guid] = new GeneratorWorker(config, _producer);

        _workers[guid].Work();

        return guid;
    }

    public async Task<bool> UpdateAsync(
        Guid id,
        string endpoint,
        int requestsPerMinute)
    {
        if (_workers.TryGetValue(id, out GeneratorWorker? oldWorker) is false)
        {
            return false;
        }

        await oldWorker.DisposeAsync();

        GeneratorConfig newConfig = oldWorker.Config with
        {
            Endpoint = new Endpoint(endpoint),
            RequestsPerMinute = requestsPerMinute,
        };

        _workers[id] = new GeneratorWorker(newConfig, _producer);
        _workers[id].Work();

        return true;
    }

    public async ValueTask DisposeAsync()
    {
        await Task.WhenAll(
            _workers.Values.Select(worker => worker.DisposeAsync().AsTask()));

        _workers.Clear();
    }
}
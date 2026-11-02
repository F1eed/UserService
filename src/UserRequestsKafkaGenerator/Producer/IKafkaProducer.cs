namespace Itmo.HighLoad.UserRequestsKafkaGenerator.Producer;

public interface IKafkaProducer<in TMessage> : IDisposable
{
    Task ProduceAsync(int key, TMessage message, CancellationToken cancellationToken);
}
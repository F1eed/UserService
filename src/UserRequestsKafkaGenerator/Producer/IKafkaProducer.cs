namespace Itmo.HighLoad.UserRequestsKafkaGenerator.Producer;

public interface IKafkaProducer<in TMessage> : IDisposable
{
    Task ProduceAsync(TMessage message, CancellationToken cancellationToken);
}
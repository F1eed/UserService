namespace Itmo.HighLoad.UserRequestsKafkaGenerator.Options;

public class KafkaOptions
{
    public required string BootstrapService { get; init; }

    public required string Topic { get; init; }
}
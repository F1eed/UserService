using Itmo.HighLoad.UserRequestsKafkaGenerator.Models;

namespace Itmo.HighLoad.UserRequestsKafkaGenerator.Generator;

public sealed record GeneratorConfig(
    Guid Id,
    UserId UserId,
    Endpoint Endpoint,
    int RequestsPerMinute);
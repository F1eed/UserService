using SourceKit.Generators.Builder.Annotations;

namespace Itmo.HighLoad.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public sealed partial record UserQueryById(long Id);
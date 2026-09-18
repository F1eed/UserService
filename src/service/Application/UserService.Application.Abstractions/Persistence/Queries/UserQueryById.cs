using SourceKit.Generators.Builder.Annotations;

namespace UserService.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public sealed record UserQueryById(Guid Id);
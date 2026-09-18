using SourceKit.Generators.Builder.Annotations;

namespace UserService.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public record UserQueryByNameAndSurname(string Name, string Surname);
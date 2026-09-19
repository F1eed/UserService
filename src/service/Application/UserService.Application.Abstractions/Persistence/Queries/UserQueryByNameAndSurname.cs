using SourceKit.Generators.Builder.Annotations;

namespace UserService.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public partial record UserQueryByNameAndSurname(string Name, string Surname);
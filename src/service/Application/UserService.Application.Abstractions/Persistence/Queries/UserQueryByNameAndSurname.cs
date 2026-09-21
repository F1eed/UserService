using SourceKit.Generators.Builder.Annotations;

namespace Itmo.HighLoad.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public partial record UserQueryByNameAndSurname(string Name, string Surname);
using SourceKit.Generators.Builder.Annotations;

namespace UserService.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public partial record UserUpdateQuery(long Id, string? Password, string? Name, string? Surname, DateOnly? DateOfBirth);
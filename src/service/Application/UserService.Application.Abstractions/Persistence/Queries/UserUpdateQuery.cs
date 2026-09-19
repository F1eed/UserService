using SourceKit.Generators.Builder.Annotations;

namespace UserService.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public partial record UserUpdateQuery(string Password, string Name, string Surname, DateTime DateOfBirth);
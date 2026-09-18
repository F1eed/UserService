using SourceKit.Generators.Builder.Annotations;

namespace UserService.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public record UserUpdateQuery(string Password, string Name, string Surname, DateTime DateOfBirth);
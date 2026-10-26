using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace Itmo.HighLoad.UserRequestsKafkaGenerator.Commands.Settings;

public class AddCommandSettings : CommandSettings
{
    [CommandArgument(0, "<User id>")]
    public required int UserId { get; init; }

    [CommandArgument(1, "<Endpoint>")]
    public required string Endpoint { get; init; }

    [CommandArgument(2, "<RPM>")]
    [Description("Requests per minute")]
    public required int RequestsPerMinute { get; init; }

    public override ValidationResult Validate()
    {
        if (UserId <= 0)
        {
            return ValidationResult.Error("Incorrect User id");
        }

        if (string.IsNullOrWhiteSpace(Endpoint.Trim()))
        {
            return ValidationResult.Error("Incorrect endpoint");
        }

        if (RequestsPerMinute <= 0)
        {
            return ValidationResult.Error("Incorrect RPM");
        }

        return ValidationResult.Success();
    }
}
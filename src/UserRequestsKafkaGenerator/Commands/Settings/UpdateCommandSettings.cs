using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace Itmo.HighLoad.UserRequestsKafkaGenerator.Commands.Settings;

public class UpdateCommandSettings : CommandSettings
{
    [CommandArgument(0, "<Worker id>")]
    public required Guid WorkerId { get; init; }

    [CommandArgument(1, "<Endpoint>")]
    public required string Endpoint { get; init; }

    [CommandArgument(2, "<RPM>")]
    [Description("Requests per minute")]
    public required int RequestsPerMinute { get; init; }

    public override ValidationResult Validate()
    {
        if (WorkerId == Guid.Empty)
        {
            return ValidationResult.Error("Incorrect worker id");
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
using Itmo.HighLoad.UserRequestsKafkaGenerator.Commands.Settings;
using Itmo.HighLoad.UserRequestsKafkaGenerator.Generator;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Itmo.HighLoad.UserRequestsKafkaGenerator.Commands;

public class AddCommand : Command<AddCommandSettings>
{
    private readonly GeneratorManager _generatorManager;

    public AddCommand(GeneratorManager generatorManager)
    {
        _generatorManager = generatorManager;
    }

    public override int Execute(
        CommandContext context,
        AddCommandSettings settings,
        CancellationToken cancellationToken)
    {
        Guid guid = _generatorManager.AddWorker(
            settings.UserId, settings.Endpoint, settings.RequestsPerMinute);

        AnsiConsole.MarkupLine($"[green]Successfully created: {guid}[/]");

        return 0;
    }
}
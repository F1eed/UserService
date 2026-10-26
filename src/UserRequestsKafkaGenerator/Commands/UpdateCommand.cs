using Itmo.HighLoad.UserRequestsKafkaGenerator.Commands.Settings;
using Itmo.HighLoad.UserRequestsKafkaGenerator.Generator;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Itmo.HighLoad.UserRequestsKafkaGenerator.Commands;

public class UpdateCommand : AsyncCommand<UpdateCommandSettings>
{
    private readonly GeneratorManager _manager;

    public UpdateCommand(GeneratorManager manager)
    {
        _manager = manager;
    }

    public override async Task<int> ExecuteAsync(
        CommandContext context,
        UpdateCommandSettings settings,
        CancellationToken cancellationToken)
    {
        if (await _manager.UpdateAsync(
                settings.WorkerId,
                settings.Endpoint,
                settings.RequestsPerMinute) is false)
        {
            AnsiConsole.MarkupLine($"[red]ERROR: Worker {settings.WorkerId} is not found");
            return 1;
        }

        AnsiConsole.MarkupLine($"[green]Successfully update[/]");
        return 0;
    }
}
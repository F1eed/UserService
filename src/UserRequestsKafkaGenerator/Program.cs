using Itmo.HighLoad.UserRequestsKafkaGenerator;
using Itmo.HighLoad.UserRequestsKafkaGenerator.Commands;
using Itmo.HighLoad.UserRequestsKafkaGenerator.Generator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

IServiceCollection collection =
    new ServiceCollection().AddKafkaGenerator(configuration);

await using ServiceProvider provider = collection.BuildServiceProvider();

GeneratorManager manager = provider.GetRequiredService<GeneratorManager>();

var commandService = new ServiceCollection();
commandService.AddSingleton(manager);

var registrar = new ServiceCollectionRegistrar(commandService);

var app = new CommandApp(registrar);

app.Configure(config =>
{
    config.AddCommand<AddCommand>("add");
    config.AddCommand<UpdateCommand>("update");
});

while (true)
{
    string input = AnsiConsole.Ask<string>($"[yellow]> [/]");

    if (input == "exit")
    {
        break;
    }

    await app.RunAsync(input.Split(' '));
}
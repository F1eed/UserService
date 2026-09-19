using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Abstractions.Persistence.Repositories;
using UserService.Infrastructure.Persistence.Options;

namespace UserService.Infrastructure.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructurePersistence(
        this IServiceCollection collection,
        IConfiguration configuration)
    {
        collection.AddOptions<PostgresConnectionOptions>()
            .BindConfiguration("Infrastructure:Persistence:Postgres")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        IConfigurationSection postgresSection = configuration.GetSection("Infrastructure:Persistence:Postgres");

        collection.AddOptions<PostgresConnectionOptions>()
            .BindConfiguration("Infrastructure:Persistence:Postgres")
            .ValidateOnStart();

        PostgresConnectionOptions postgresOptions = postgresSection.Get<PostgresConnectionOptions>()
            ?? throw new InvalidOperationException("Postgres configuration not found.");

        string connectionString = postgresOptions.ToConnectionString();

        collection.AddNpgsqlDataSource(connectionString);

        collection
            .AddFluentMigratorCore()
            .ConfigureRunner(runner => runner
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .WithMigrationsIn(typeof(IAssemblyMarker).Assembly))
            .AddLogging(logging => logging.AddFluentMigratorConsole());

        collection.AddSingleton<IUserRepository, UserRepository>();

        return collection;
    }
}
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Abstractions.Persistence.Repositories;
using UserService.Application.Abstractions.Persistence.Transactions;
using UserService.Infrastructure.Persistence.Options;
using UserService.Infrastructure.Persistence.Transactions;

namespace UserService.Infrastructure.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructurePersistence(
        this IServiceCollection collection,
        IConfiguration configuration)
    {
        IConfigurationSection postgresSection = configuration.GetSection("Infrastructure:Persistence:Postgres");

        collection.AddOptions<PostgresConnectionOptions>()
            .BindConfiguration("Infrastructure:Persistence:Postgres")
            .ValidateDataAnnotations()
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

        collection.AddScoped<IUserRepository, UserRepository>();

        collection.AddScoped<IPersistenceTransactionProvider, PostgresPersistenceTransactionProvider>();

        return collection;
    }
}
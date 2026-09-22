using Itmo.HighLoad.Application.Abstractions.Persistence.Repositories;
using Itmo.HighLoad.Application.Model.Users;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using UserQueryByNameAndSurname = Itmo.HighLoad.Application.Abstractions.Persistence.Queries.UserQueryByNameAndSurname;
using UserUpdateQuery = Itmo.HighLoad.Application.Abstractions.Persistence.Queries.UserUpdateQuery;

namespace Itmo.HighLoad.Infrastructure.Persistence;

internal class UserRepository : IUserRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public UserRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async IAsyncEnumerable<User> AddUsersAsync(
        IReadOnlyCollection<User> users,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (users.Count == 0)
        {
            yield break;
        }

        await using NpgsqlConnection connection = await _dataSource
            .OpenConnectionAsync(cancellationToken);

        const string sql = """
        INSERT INTO users (user_login, user_password, user_name, user_surname, user_date_of_birth)
        SELECT * FROM UNNEST(:user_logins, :user_passwords, :user_names, :user_surnames, :users_date_of_birth)
        RETURNING user_id, user_login, user_password, user_name, user_surname, user_date_of_birth
        """;

        string[] logins = [.. users.Select(x => x.Login.Value)];
        string[] passwords = [.. users.Select(x => x.Password.Value)];
        string[] names = [.. users.Select(x => x.Name)];
        string[] surnames = [.. users.Select(x => x.Surname)];
        DateOnly?[] dates = [.. users.Select(x => x.DateOfBirth?.Value)];

        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.Add(new NpgsqlParameter("user_logins", logins));
        command.Parameters.Add(new NpgsqlParameter("user_passwords", passwords));
        command.Parameters.Add(new NpgsqlParameter("user_names", names));
        command.Parameters.Add(new NpgsqlParameter("user_surnames", surnames));
        command.Parameters.Add(new NpgsqlParameter("users_date_of_birth", dates));

        await using DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            yield return MapReaderToUser(reader);
        }
    }

    public async Task<User?> QueryByIdAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        const string sql = """
        SELECT user_id,
            user_login,
            user_password,
            user_name,
            user_surname,
            user_date_of_birth
        FROM users
        WHERE (user_id = :id)
        """;

        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.Add(new NpgsqlParameter("id", userId));
        await using DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        if (await reader.ReadAsync(cancellationToken) is false)
        {
            return null;
        }

        return MapReaderToUser(reader);
    }

    public async IAsyncEnumerable<User> QueryByNameAndSurnameAsync(
        UserQueryByNameAndSurname query,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        const string sql = """
        SELECT user_id,
            user_login,
            user_password,
            user_name,
            user_surname,
            user_date_of_birth
        FROM users
        WHERE (:user_name = user_name) AND (:user_surname = user_surname)
        """;

        string name = query.Name;
        string surname = query.Surname;

        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.Add(new NpgsqlParameter("user_name", name));
        command.Parameters.Add(new NpgsqlParameter("user_surname", surname));
        await using DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            yield return MapReaderToUser(reader);
        }
    }

    public async Task<bool> UpdateUserAsync(
        UserUpdateQuery query,
        CancellationToken cancellationToken)
    {
        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        const string sql = """
        UPDATE users 
        SET user_password = COALESCE(:password, user_password),
            user_name = COALESCE(:name, user_name),
            user_surname = COALESCE(:surname, user_surname),
            user_date_of_birth = COALESCE (:date_of_birth, user_date_of_birth),
            updated_at = now()
        WHERE (user_id = :id)
        """;

        long userId = query.Id;
        string? name = query.Name;
        string? surname = query.Surname;
        string? password = query.Password;
        DateOnly? dateOfBirth = query.DateOfBirth;

        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.Add(new NpgsqlParameter("id", userId));
        command.Parameters.Add(new NpgsqlParameter<string?>("name", name));
        command.Parameters.Add(new NpgsqlParameter<string?>("surname", surname));
        command.Parameters.Add(new NpgsqlParameter<string?>("password", password));
        command.Parameters.Add(new NpgsqlParameter<DateOnly?>("date_of_birth", dateOfBirth));

        return await command.ExecuteNonQueryAsync(cancellationToken) > 0;
    }

    public async Task DeleteUserAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);

        const string sql = """
        DELETE FROM users
        WHERE (user_id = :id)
        """;

        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.Add(new NpgsqlParameter("id", userId));

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static User MapReaderToUser(DbDataReader reader)
    {
        int birthDateOrdinal = reader.GetOrdinal("user_date_of_birth");

        DateOnly? birthDate = reader.IsDBNull(birthDateOrdinal)
            ? null
            : DateOnly.FromDateTime(reader.GetDateTime("user_date_of_birth"));

        return User.CreateFromDb(
            reader.GetInt64("user_id"),
            reader.GetString("user_login"),
            reader.GetString("user_password"),
            reader.GetString("user_name"),
            reader.GetString("user_surname"),
            birthDate);
    }
}
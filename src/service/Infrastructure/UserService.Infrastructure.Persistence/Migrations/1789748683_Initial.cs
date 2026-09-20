using FluentMigrator;

namespace UserService.Infrastructure.Persistence.Migrations;

[Migration(1789748683, "Initial")]
public class Initial : Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("user_id").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("user_login").AsString().NotNullable().Unique()
            .WithColumn("user_password").AsString().NotNullable()
            .WithColumn("user_name").AsString().NotNullable()
            .WithColumn("user_surname").AsString().NotNullable()
            .WithColumn("user_date_of_birth").AsDateTime()
            .WithColumn("created_at").AsDateTime().NotNullable()
                .WithDefault(SystemMethods.CurrentDateTimeOffset)
            .WithColumn("updated_at").AsDate().Nullable();

        Execute.Sql("""
        ALTER TABLE users
        ADD CONSTRAINT ck_user_login_length
        CHECK (char_length(user_login) > 2),
        ADD CONSTRAINT ck_user_password_length
        CHECK (char_length(user_password) > 6)
        """);
    }

    public override void Down()
    {
        Delete.Table("users").IfExists();
    }
}
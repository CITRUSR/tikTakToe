using server.Infrastructure.Sql.Tables;

namespace server.Infrastructure.Sql.Queries;

public static class UserQueries
{
    private static readonly string UserSelectClause =
        @$"
        SELECT {UserTable.Id}, {UserTable.Nickname}, {UserTable.Password}
        FROM {UserTable.TableName}
    ";

    public static readonly string GetAll =
        @$"
        {UserSelectClause}
    ";

    public static readonly string GetById =
        @$"
        {UserSelectClause}
        WHERE {UserTable.Id} = @UserId
    ";

    public static readonly string GetByNickname =
        @$"
        {UserSelectClause}
        WHERE {UserTable.Nickname} = @Nickname 
    ";

    public static readonly string Insert =
        @$"
        INSERT INTO {UserTable.TableName} ({UserTable.Id}, {UserTable.Nickname}, {UserTable.Password})
        VALUES (@Id, @Nickname, @Password)
    ";

    public static readonly string Update =
        @$"
        UPDATE {UserTable.TableName}
        SET {UserTable.Nickname} = @Nickname,
        {UserTable.Password} = @Password
        WHERE {UserTable.Id} = @Id
        RETURNING {UserTable.Id}, {UserTable.Nickname}, {UserTable.Password}
    ";
}

using server.Infrastructure.Sql.Tables;

namespace server.Infrastructure.Sql.Queries;

public static class UserQueries
{
    public static readonly string AllColumns =
        @$"
        {UserTable.Id}, {UserTable.Nickname}, {UserTable.Password}
    ";

    public static readonly string GetAll =
        @$"
        SELECT {AllColumns} FROM {UserTable.TableName}
    ";

    public static readonly string GetById =
        @$"
        {GetAll}
        WHERE {UserTable.Id} = @UserId
    ";

    public static readonly string GetByNickname =
        @$"
        {GetAll}
        WHERE {UserTable.Nickname} = @Nickname 
    ";

    public static readonly string Insert =
        @$"
        INSERT INTO {UserTable.TableName} ({AllColumns})
        VALUES (@Id, @Nickname, @Password)
    ";

    public static readonly string Update =
        @$"
        UPDATE {UserTable.TableName}
        SET {UserTable.Nickname} = @Nickname,
        {UserTable.Password} = @Password
        WHERE {UserTable.Id} = @Id
        RETURNING {AllColumns}
    ";
}

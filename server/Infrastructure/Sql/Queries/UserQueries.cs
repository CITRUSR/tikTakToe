using server.Infrastructure.Sql.Tables;

namespace server.Infrastructure.Sql.Queries;

public static class UserQueries
{
    public static readonly string GetAll =
        @$"
        SELECT {UserTable.Id}, {UserTable.Nickname}, {UserTable.Password}
        FROM {UserTable.TableName}
    ";

    public static readonly string GetById =
        @$"
        SELECT {UserTable.Id}, {UserTable.Nickname}, {UserTable.Password}
        FROM {UserTable.TableName}
        WHERE {UserTable.Id} = @UserId
    ";
}

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
}

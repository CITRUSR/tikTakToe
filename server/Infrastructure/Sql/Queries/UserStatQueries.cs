using server.Infrastructure.Sql.Tables;

namespace server.Infrastructure.Sql.Queries;

public static class UserStatQueries
{
    public static readonly string Get =
        @$"
        SELECT {UserStatTable.TableName}.{UserStatTable.UserId},
        {UserStatTable.TableName}.{UserStatTable.Wins},
        {UserStatTable.TableName}.{UserStatTable.Losses},
        {UserStatTable.TableName}.{UserStatTable.GamesCount},
        {UserTable.TableName}.{UserTable.Id} AS id,
        {UserTable.TableName}.{UserTable.Nickname},
        {UserTable.TableName}.{UserTable.Password}
        FROM {UserStatTable.TableName}
        JOIN {UserTable.TableName}
        ON {UserStatTable.TableName}.{UserStatTable.UserId} = {UserTable.TableName}.{UserTable.Id}
        WHERE {UserStatTable.TableName}.{UserStatTable.UserId} = @UserId
    ";

    public static readonly string Update =
        @$"
        WITH user AS(
            {UserQueries.GetAll}
        )
        UPDATE {UserStatTable.TableName}
        SET {UserStatTable.Wins} = @Wins,
            {UserStatTable.Losses} = @Losses,
            {UserStatTable.GamesCount} = @GamesCount
        WHERE {UserStatTable.UserId} = @UserId
        RETURNING
        {UserStatTable.UserId},
        {UserStatTable.Wins},
        {UserStatTable.Losses},
        {UserStatTable.GamesCount},
        {UserTable.Id},
        {UserTable.Nickname},
        {UserTable.Password}
    ";
}

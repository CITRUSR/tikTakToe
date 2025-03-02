using server.Infrastructure.Sql.Tables;

namespace server.Infrastructure.Sql.Queries;

public static class UserStatQueries
{
    public static readonly string AllColumns =
        $"{UserStatTable.UserId}, {UserStatTable.Wins}, {UserStatTable.Losses}, {UserStatTable.GamesCount} AS gamesCount";

    public static readonly string GetAll =
        @$"
        SELECT {AllColumns} FROM {UserStatTable.TableName}
    ";

    public static readonly string Get =
        @$"
        SELECT {AllColumns},
        {UserQueries.AllColumns}
        FROM {UserStatTable.TableName}
        JOIN {UserTable.TableName}
        ON {UserStatTable.UserId} = {UserTable.Id}
        WHERE {UserStatTable.UserId} = @UserId
    ";

    public static readonly string Update =
        @$"
        WITH user_data AS(
            {UserQueries.GetAll}
        ),
        updated_stats AS(
            UPDATE {UserStatTable.TableName}
            SET {UserStatTable.Wins} = @Wins,
            {UserStatTable.Losses} = @Losses,
            {UserStatTable.GamesCount} = @GamesCount
            WHERE {UserStatTable.UserId} = @UserId
            RETURNING {AllColumns}
        )
        SELECT updated_stats.{UserStatTable.UserId},
        updated_stats.{UserStatTable.Wins},
        updated_stats.{UserStatTable.Losses},
        updated_stats.gamesCount,
        user_data.{UserTable.Id},
        user_data.{UserTable.Nickname}
        FROM user_data
        JOIN updated_stats ON updated_stats.{UserStatTable.UserId} = user_data.{UserTable.Id}
    ";
}

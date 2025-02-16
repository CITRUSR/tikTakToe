using server.Infrastructure.Sql.Tables;

namespace server.Infrastructure.Sql.Queries;

public static class RefreshTokenQueries
{
    private static readonly string AllColumns =
        $"{RefreshTokenTable.UserId}, {RefreshTokenTable.Token}, {RefreshTokenTable.ExpiresAt}";

    public static readonly string Insert =
        @$"
        INSERT INTO {RefreshTokenTable.TableName} ({AllColumns})
        VALUES (@UserId, @Token, @ExpiresAt)
        RETURNING {AllColumns}
    ";

    public static readonly string Update =
        @$"
        UPDATE {RefreshTokenTable.TableName}
        SET {RefreshTokenTable.Token} = @Token,
        {RefreshTokenTable.ExpiresAt} = @ExpiresAt
        WHERE {RefreshTokenTable.UserId} = @UserId
        RETURNING {AllColumns}
    ";

    public static readonly string GetByUserId =
        @$"
        SELECT {AllColumns} FROM {RefreshTokenTable.TableName}
        WHERE {RefreshTokenTable.UserId} = @UserId
    ";
}

using server.Infrastructure.Sql.Tables;

namespace server.Infrastructure.Sql.Queries;

public static class RefreshTokenQueries
{
    public static readonly string Insert =
        @$"
        INSERT INTO {RefreshTokenTable.TableName} ({RefreshTokenTable.UserId}, {RefreshTokenTable.Token}, {RefreshTokenTable.ExpiresAt})
        VALUES (@UserId, @Token, @ExpiresAt)
        RETURNING {RefreshTokenTable.UserId}, {RefreshTokenTable.Token}, {RefreshTokenTable.ExpiresAt}
    ";
}

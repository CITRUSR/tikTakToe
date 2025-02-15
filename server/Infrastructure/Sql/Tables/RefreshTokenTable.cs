namespace server.Infrastructure.Sql.Tables;

public static class RefreshTokenTable
{
    public static readonly string TableName = "refresh_tokens";
    public static readonly string UserId = "user_id";
    public static readonly string Token = "token";
    public static readonly string ExpiresAt = "expires_at";
}

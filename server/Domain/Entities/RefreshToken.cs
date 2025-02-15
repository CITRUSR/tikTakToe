namespace server.Domain.Entities;

public class RefreshToken
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}

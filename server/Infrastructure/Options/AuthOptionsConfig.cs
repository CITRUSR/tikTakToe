namespace server.Infrastructure.Options;

public class AuthOptionsConfig
{
    public string Audience { get; set; }

    public string Issuer { get; set; }
    public int LifeTimeInSeconds { get; set; }
    public string Key { get; set; }
}

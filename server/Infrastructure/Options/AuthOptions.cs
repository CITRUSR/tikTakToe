using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using server.Application.Options;

namespace server.Infrastructure.Options;

public class AuthOptions : IAuthOptions
{
    public string Audience { get; }
    public string Issuer { get; }
    public int LifeTimeInSeconds { get; }
    public int LifeTimeRefreshInDays { get; }
    public string Key { get; }

    public AuthOptions(IOptions<AuthOptionsConfig> config)
    {
        AuthOptionsConfig _config = config.Value;
        Audience = _config.Audience ?? throw new ArgumentException("Audience is not set");
        Issuer = _config.Issuer ?? throw new ArgumentException("Issuer is not set");
        Key = _config.Key ?? throw new ArgumentException("Key is not set");
        LifeTimeInSeconds = _config.LifeTimeInSeconds;
        LifeTimeRefreshInDays = _config.LifeTimeRefreshInDays;
    }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}

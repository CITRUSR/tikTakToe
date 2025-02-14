using Microsoft.IdentityModel.Tokens;

namespace server.Application.Options;

public interface IAuthOptions
{
    string Audience { get; }
    string Issuer { get; }
    string Key { get; }
    int LifeTimeInSeconds { get; }
    SymmetricSecurityKey GetSymmetricSecurityKey();
}

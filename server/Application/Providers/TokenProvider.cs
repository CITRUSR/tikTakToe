using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using server.Application.Contracts.Providers;
using server.Application.Options;
using server.Domain.Entities;

namespace server.Application.Providers;

public class TokenProvider(IAuthOptions options) : ITokenProvider
{
    private readonly IAuthOptions _authOptions = options;

    public string GenerateAccessToken(User user)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.Name, user.Nickname),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        ];

        var token = new JwtSecurityToken(
            issuer: _authOptions.Issuer,
            audience: _authOptions.Audience,
            expires: DateTime.Now.AddSeconds(_authOptions.LifeTimeInSeconds),
            signingCredentials: new SigningCredentials(
                _authOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256
            ),
            claims: claims
        );

        var handler = new JwtSecurityTokenHandler();

        return handler.WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken(Guid userId)
    {
        byte[] token = new byte[16];

        using RandomNumberGenerator rng = RandomNumberGenerator.Create();

        rng.GetBytes(token);

        string base64Token = Convert.ToBase64String(token);

        return new RefreshToken
        {
            UserId = userId,
            Token = base64Token,
            ExpiresAt = DateTime.Now.AddDays(_authOptions.LifeTimeRefreshInDays),
        };
    }
}

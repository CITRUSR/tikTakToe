using server.Application.Common.Exceptions.Auth;

namespace server.Domain.Exceptions.RefreshToken;

public class InvalidRefreshTokenException : IdentityException
{
    public InvalidRefreshTokenException()
        : base("Invalid refresh token") { }
}

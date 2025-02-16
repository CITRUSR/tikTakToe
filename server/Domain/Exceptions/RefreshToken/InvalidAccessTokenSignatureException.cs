using server.Application.Common.Exceptions.Auth;

namespace server.Domain.Exceptions.RefreshToken;

public class InvalidAccessTokenSignatureException : IdentityException
{
    public InvalidAccessTokenSignatureException()
        : base("Invalid access token signature") { }
}

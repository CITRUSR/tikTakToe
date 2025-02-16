using server.Application.Common.Exceptions.Auth;

namespace server.Domain.Exceptions.RefreshToken;

public class InvalidAccessTokenException : IdentityException
{
    public InvalidAccessTokenException(string msg)
        : base(msg) { }
}

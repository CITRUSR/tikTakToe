namespace server.Application.Common.Exceptions.Auth;

public class IdentityException : Exception
{
    public IdentityException(string msg)
        : base(msg) { }
}

namespace server.Application.Common.Exceptions.GameSession;

public class SessionGameException : Exception
{
    public string ErrorMessage { get; set; }

    public SessionGameException(string msg)
        : base(msg)
    {
        ErrorMessage = msg;
    }
}

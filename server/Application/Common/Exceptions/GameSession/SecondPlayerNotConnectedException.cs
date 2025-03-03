namespace server.Application.Common.Exceptions.GameSession;

public class SecondPlayerNotConnectedException : SessionGameException
{
    public SecondPlayerNotConnectedException()
        : base("Second player not connected") { }
}

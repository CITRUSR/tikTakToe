namespace server.Application.Common.Exceptions.GameSession;

public class SamePlayerNextTurnException : SessionGameException
{
    public SamePlayerNextTurnException()
        : base("Active player for next move must be different") { }
}

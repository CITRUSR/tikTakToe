namespace server.Application.Common.Exceptions.GameSession;

public class IncorrectActorException : SessionGameException
{
    public IncorrectActorException()
        : base("Current step for another player") { }
}

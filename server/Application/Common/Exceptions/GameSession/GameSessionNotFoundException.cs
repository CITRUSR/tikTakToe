namespace server.Application.Common.Exceptions.GameSession;

public class GameSessionNotFoundException : SessionGameException
{
    public GameSessionNotFoundException(Guid gameId)
        : base($"Game session with id:{gameId} not found") { }
}

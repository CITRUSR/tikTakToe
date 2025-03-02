using server.Application.Common.Exceptions.GameSession;
using server.Domain.Exceptions;

namespace server.Application.Common.Exceptions.Map;

public class MapNotFoundException : SessionGameException
{
    public MapNotFoundException(Guid gameId)
        : base($"Map for game with id:{gameId} not found") { }
}

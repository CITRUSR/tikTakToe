using server.Domain.Entities;

namespace server.Application.Services.GameSession.Dtos.Requests.UpdateGameSession;

public record UpdateGameSessionRequest(
    Guid GameId,
    UserConnection ActivePlayerConnection,
    bool GameInProcess,
    Guid? WinnerId = null,
    TimeSpan? EndTime = null
);

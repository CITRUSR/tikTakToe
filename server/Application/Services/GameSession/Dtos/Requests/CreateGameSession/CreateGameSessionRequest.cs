using server.Domain.Entities;

namespace server.Application.Services.GameSession.Dtos.Requests.CreateGameSession;

public record CreateGameSessionRequest(List<UserConnection> Players);

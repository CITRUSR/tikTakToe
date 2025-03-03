using server.Domain.Entities;

namespace server.Application.Services.GameSession.Dtos;

public record SocketMessageEnvelope(
    UserConnection UserConnection,
    Domain.Entities.Room Room,
    string Message,
    string Type
);

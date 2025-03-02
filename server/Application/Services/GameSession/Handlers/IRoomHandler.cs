using server.Domain.Entities;

namespace server.Application.Services.GameSession.Handlers;

public interface IRoomHandler
{
    string Type { get; }
    Task HandleAsync(Domain.Entities.Room room, UserConnection userConnection, string msg);
}

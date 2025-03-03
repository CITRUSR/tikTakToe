using server.Domain.Entities;

namespace server.Application.Contracts.Services;

public interface IGameService
{
    Task<Game> HandleGameAsync(List<UserConnection> usersConnections);
    bool TryStartGame(Guid roomId);
}

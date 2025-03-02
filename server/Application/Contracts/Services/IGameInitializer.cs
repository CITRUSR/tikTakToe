using server.Application.Services.GameSession.Dtos;
using server.Domain.Entities;

namespace server.Application.Contracts.Services;

public interface IGameInitializer
{
    Task<Guid> BeginGameAsync(List<UserConnection> userConnections);
}

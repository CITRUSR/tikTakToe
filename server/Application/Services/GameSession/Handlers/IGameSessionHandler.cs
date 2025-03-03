using server.Application.Services.GameSession.Dtos;
using server.Domain.Entities;

namespace server.Application.Services.GameSession.Handlers;

public interface IGameSessionHandler
{
    public string Type { get; }
    Task HandleAsync(GameSessionDto game, UserConnection sender, string msg);
}

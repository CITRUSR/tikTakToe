using server.Application.Contracts.Services;
using server.Domain.Entities;

namespace server.Application.Services.GameSession.Handlers;

public class StartSocketMessageHandler(IGameService gameSessionService) : IRoomHandler
{
    public string Type => "Start";
    private readonly IGameService _gameSessionService = gameSessionService;

    public Task HandleAsync(Domain.Entities.Room room, UserConnection userConnection, string msg)
    {
        var isGameStarted = _gameSessionService.TryStartGame(room.Id);

        return Task.CompletedTask;
    }
}

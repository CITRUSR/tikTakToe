using server.Application.Contracts.Repositories;
using server.Application.Contracts.Services;
using server.Domain.Entities;

namespace server.Application.Services.GameSession;

public class GameService(
    IGameFinisher gameFinisher,
    IGameInitializer gameInitializer,
    IGameSessionOrchestrator gameSessionOrchestrator,
    IRoomConnectionStorage roomConnectionStorage,
    IGameSessionSocketHelper socketHelper
) : IGameService
{
    private readonly IGameInitializer _gameInitializer = gameInitializer;
    private readonly IGameSessionOrchestrator _gameSessionOrchestrator = gameSessionOrchestrator;
    private readonly IGameFinisher _gameFinisher = gameFinisher;
    private readonly IRoomConnectionStorage _roomConnectionStorage = roomConnectionStorage;
    private readonly IGameSessionSocketHelper _socketHelper = socketHelper;

    public async Task<Game> HandleGameAsync(List<UserConnection> usersConnections)
    {
        if (!CheckConnectionCount(usersConnections))
        {
            await _socketHelper.SendErrorToClientsAsync(
                usersConnections,
                "Need more players to start game"
            );
        }

        var sessionGameId = await _gameInitializer.BeginGameAsync(usersConnections);

        await _gameSessionOrchestrator.HandleGameAsync(sessionGameId);

        return await _gameFinisher.FinishGameAsync(sessionGameId);
    }

    public bool TryStartGame(Guid roomId)
    {
        var connections = _roomConnectionStorage.Get(roomId);

        if (CheckConnectionCount(connections))
        {
            _ = Task.Run(() => HandleGameAsync(connections));

            return true;
        }

        return false;
    }

    private bool CheckConnectionCount(List<UserConnection> connections)
    {
        return connections.Count == 2;
    }
}

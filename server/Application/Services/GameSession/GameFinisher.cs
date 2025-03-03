using System.Text.Json;
using Mapster;
using server.Application.Contracts.Services;
using server.Application.Services.GameSession.Dtos.Requests.DeleteGameSession;
using server.Application.Services.GameSession.Dtos.Requests.GetGameSession;
using server.Application.Services.GameSession.Dtos.SocketMessages.Server;
using server.Application.Services.UserStat.Dtos.Requests.GetUserStat;
using server.Application.Services.UserStat.Dtos.Requests.UpdateUserStat;
using server.Domain.Entities;
using server.Handlers.Sockets;

namespace server.Application.Services.GameSession;

public class GameFinisher(
    IGameSessionSocketHelper socketHelper,
    ISocketHandler socketHandler,
    IGameSessionService gameSessionService,
    IUserStatService userStatService
) : IGameFinisher
{
    private readonly IGameSessionSocketHelper _socketHelper = socketHelper;
    private readonly ISocketHandler _socketHandler = socketHandler;
    private readonly IGameSessionService _gameSessionService = gameSessionService;
    private readonly IUserStatService _userStatService = userStatService;

    public async Task<Game> FinishGameAsync(Guid gameSessionId)
    {
        var gameSession = await _gameSessionService.GetAsync(
            new GetGameSessionRequest(gameSessionId)
        );

        var gameResult = gameSession.Adapt<Game>();

        gameResult.EndTime = DateTime.Now.TimeOfDay;

        var finishMsg = new FinishSocketMessage { Game = gameResult };

        var msg = JsonSerializer.Serialize(finishMsg);

        await _socketHelper.SendToAllClientsAsync(gameSession.PlayerConnections, msg);

        List<Task> closeTasks = [];

        foreach (var player in gameSession.PlayerConnections)
        {
            var stats = await _userStatService.GetAsync(new GetUserStatRequest(player.User.Id));

            (int wins, int losses) updates =
                gameSession.Winner == null
                    ? (stats.Wins, stats.Losses)
                    : (
                        player.User.Id == gameSession.Winner.Id ? stats.Wins + 1 : stats.Wins,
                        player.User.Id != gameSession.Winner.Id ? stats.Losses + 1 : stats.Losses
                    );

            await _userStatService.UpdateAsync(
                new UpdateUserStatRequest(
                    player.User.Id,
                    updates.wins,
                    updates.losses,
                    stats.GamesCount + 1
                )
            );

            closeTasks.Add(_socketHandler.CloseAsync(player.WebSocket));
        }

        await Task.WhenAll(closeTasks);

        await _gameSessionService.DeleteAsync(new DeleteGameSessionRequest(gameSessionId));

        return gameResult;
    }
}

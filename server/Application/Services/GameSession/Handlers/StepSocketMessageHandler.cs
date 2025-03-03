using System.Text.Json;
using Mapster;
using server.Application.Common.Exceptions.GameSession;
using server.Application.Contracts.Providers;
using server.Application.Contracts.Services;
using server.Application.Services.GameSession.Dtos;
using server.Application.Services.GameSession.Dtos.Requests.UpdateGameSession;
using server.Application.Services.GameSession.Dtos.SocketMessages.Server;
using server.Application.Services.Map.Dtos;
using server.Domain.Entities;

namespace server.Application.Services.GameSession.Handlers;

public class StepSocketMessageHandler(
    IGameSessionSocketHelper socketHelper,
    IGameRules gameRules,
    IMapService mapService,
    IGameSessionService gameSessionService,
    IGameSessionCancellationTokenProvider cancellationTokenProvider
) : IGameSessionHandler
{
    public string Type => "Step";
    private readonly IGameSessionSocketHelper _socketHelper = socketHelper;
    private readonly IGameRules _gameRules = gameRules;
    private readonly IMapService _mapService = mapService;
    private readonly IGameSessionService _gameSessionService = gameSessionService;
    private readonly IGameSessionCancellationTokenProvider _cancellationTokenProvider =
        cancellationTokenProvider;

    public async Task HandleAsync(GameSessionDto game, UserConnection sender, string msg)
    {
        if (game.ActivePlayerConnection.User.Id != sender.User.Id)
        {
            throw new SamePlayerNextTurnException();
        }

        var stepMessage = JsonSerializer.Deserialize<StepSocketMessage>(msg);

        if (stepMessage.User.Id != sender.User.Id)
        {
            throw new IncorrectActorException();
        }

        game.Map.SetCellState(stepMessage.X, stepMessage.Y, stepMessage.State, sender.User);

        await _mapService.UpdateAsync(game.Map);

        var result = _gameRules.CheckMap(game.Map);

        if (result.WinnerId.HasValue || result.IsDraw)
        {
            game.GameInProcess = false;
            game.Winner = result.WinnerId.HasValue
                ? game.PlayerConnections.FirstOrDefault(x => x.User.Id == result.WinnerId)?.User
                : null;
        }

        game.ActivePlayerConnection = game.PlayerConnections.FirstOrDefault(x =>
            x.User != game.ActivePlayerConnection.User
        );

        await _gameSessionService.UpdateAsync(
            new UpdateGameSessionRequest(
                game.Id,
                game.ActivePlayerConnection,
                game.GameInProcess,
                game.Winner?.Id,
                game.EndTime
            )
        );

        var initMsg = new InitSocketMessage
        {
            ActiveUser = game.ActivePlayerConnection.User,
            Map = game.Map.Adapt<GameBoardDto>(),
        };

        var initMsgJson = JsonSerializer.Serialize(initMsg);

        await _socketHelper.SendToAllClientsAsync(game.PlayerConnections, initMsgJson);

        if (result.WinnerId.HasValue || result.IsDraw)
        {
            await _cancellationTokenProvider.CancelAsync(game.Id);
        }
    }
}

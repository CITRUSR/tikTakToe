using System.Net.WebSockets;
using server.Application.Common.Exceptions.GameSession;
using server.Application.Contracts.Providers;
using server.Application.Contracts.Services;
using server.Application.Services.GameSession.Dtos;
using server.Application.Services.GameSession.Dtos.Requests.GetGameSession;
using server.Application.Services.GameSession.Handlers;
using server.Domain.Entities;
using server.Utils;

namespace server.Application.Services.GameSession;

public class GameSessionOrchestrator(
    IEnumerable<IGameSessionHandler> handlers,
    IUserMessagesNotifier userMessagesNotifier,
    IGameSessionService gameSessionService,
    IGameSessionSocketHelper socketHelper,
    IGameSessionCancellationTokenProvider cancellationTokenProvider
) : IGameSessionOrchestrator
{
    private readonly IEnumerable<IGameSessionHandler> _handlers = handlers;
    private readonly IUserMessagesNotifier _userMessagesNotifier = userMessagesNotifier;
    private readonly IGameSessionService _gameSessionService = gameSessionService;
    private readonly IGameSessionSocketHelper _socketHelper = socketHelper;
    private readonly IGameSessionCancellationTokenProvider _cancellationTokenProvider =
        cancellationTokenProvider;

    public async Task HandleGameAsync(Guid gameSessionId)
    {
        var gameSession = await _gameSessionService.GetAsync(
            new GetGameSessionRequest(gameSessionId)
        );

        var playerTasks = new List<Task>();

        foreach (var player in gameSession.PlayerConnections)
        {
            _userMessagesNotifier.Subscribe(
                player.User.Id,
                async (msg) =>
                {
                    await HandleMessages(msg, gameSession.Id);
                }
            );
            playerTasks.Add(
                Task.Run(
                    () =>
                        HandlePlayerAsync(
                            player,
                            _cancellationTokenProvider.GetCancellationToken(gameSessionId)
                        )
                )
            );
        }

        await Task.WhenAll(playerTasks);
    }

    private void HandlePlayerAsync(
        UserConnection userConnection,
        CancellationToken cancellationToken
    )
    {
        //for web sockets connections not disconnecting
        while (
            userConnection.WebSocket.State == WebSocketState.Open
            && !cancellationToken.IsCancellationRequested
        ) { }
    }

    private async Task HandleMessages(SocketMessageEnvelope msg, Guid gameSessionId)
    {
        var gameSession = await _gameSessionService.GetAsync(
            new GetGameSessionRequest(gameSessionId)
        );

        var type = JsonHelper.GetSocketMessageType(msg.Message);

        if (!gameSession.GameInProcess)
        {
            await _socketHelper.SendErrorToClientsAsync(
                [msg.UserConnection],
                "Game not in process"
            );
            return;
        }

        foreach (var handler in _handlers)
        {
            if (handler.Type == type)
            {
                try
                {
                    await handler.HandleAsync(gameSession, msg.UserConnection, msg.Message);
                }
                catch (SessionGameException ex)
                {
                    await _socketHelper.SendErrorToClientsAsync(
                        [msg.UserConnection],
                        ex.ErrorMessage
                    );

                    return;
                }
            }
        }
    }
}

using System.Text.Json;
using server.Application.Contracts.Services;
using server.Application.Services.GameSession.Dtos.SocketMessages.Server;
using server.Domain.Entities;
using server.Handlers.Sockets;

namespace server.Application.Services.GameSession;

public class GameSessionSocketHelper(ISocketHandler socketHandler) : IGameSessionSocketHelper
{
    private readonly ISocketHandler _socketHandler = socketHandler;

    public async Task SendErrorToClientsAsync(List<UserConnection> userConnections, string errorMsg)
    {
        var errorMessage = new ErrorSocketMessage { Message = errorMsg };

        var jsonMsg = JsonSerializer.Serialize(errorMessage);

        var tasks = userConnections.Select(x => _socketHandler.SendAsync(x.WebSocket, jsonMsg));

        await Task.WhenAll(tasks);
    }

    public async Task SendToAllClientsAsync(List<UserConnection> userConnections, string msg)
    {
        var tasks = userConnections.Select(x => _socketHandler.SendAsync(x.WebSocket, msg));

        await Task.WhenAll(tasks);
    }

    public async Task SendToAllClientsAsync(
        List<UserConnection> userConnections,
        string msg,
        List<UserConnection> exceptClients
    )
    {
        var tasks = userConnections
            .Where(x => !exceptClients.Contains(x))
            .Select(x => _socketHandler.SendAsync(x.WebSocket, msg));

        await Task.WhenAll(tasks);
    }
}

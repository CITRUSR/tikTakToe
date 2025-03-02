using System.Net.WebSockets;
using Mapster;
using server.Application.Contracts.Repositories;
using server.Application.Contracts.Services;
using server.Application.Services.GameSession;
using server.Application.Services.GameSession.Dtos;
using server.Application.Services.GameSession.Handlers;
using server.Application.Services.RoomManager.Dtos.Requests;
using server.Application.Services.RoomManager.Dtos.Requests.ConnectToRoom;
using server.Application.Services.User.Dtos.Responses;
using server.Domain.Entities;
using server.Domain.Exceptions.Room;
using server.Domain.Exceptions.User;
using server.Handlers.Sockets;
using server.Utils;

namespace server.Application.Services.RoomManager;

public class RoomManager(
    IRoomConnectionStorage connectionStorage,
    IUnitOfWork unitOfWork,
    ISocketHandler socketHandler,
    IEnumerable<IRoomHandler> roomHandlers,
    IUserMessagesNotifier userMessagesNotifier,
    IGameSessionSocketHelper socketHelper
) : IRoomManager
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRoomConnectionStorage _connectionStorage = connectionStorage;
    private readonly ISocketHandler _socketHandler = socketHandler;
    private readonly IEnumerable<IRoomHandler> _roomHandlers = roomHandlers;
    private readonly IUserMessagesNotifier _userMessagesNotifier = userMessagesNotifier;
    private readonly IGameSessionSocketHelper _socketHelper = socketHelper;

    public async Task ConnectToRoomAsync(ConnectToRoomRequest request)
    {
        var room = await _unitOfWork.RoomRepository.GetAsync(request.RoomId);

        if (room == null)
        {
            throw new RoomNotFoundException(request.RoomId);
        }

        var user = await _unitOfWork.UserRepository.GetAsync(request.UserId);

        if (user == null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        var connection = new UserConnection
        {
            User = user.Adapt<UserViewModel>(),
            WebSocket = request.Socket,
        };

        _connectionStorage.Add(room, connection);

        await HandleConnection(connection, room);
    }

    public async Task DisconnectFromRoomAsync(DisconnectFromRoomRequest request)
    {
        var room = await _unitOfWork.RoomRepository.GetAsync(request.UserId);

        if (room == null)
        {
            throw new RoomNotFoundException(request.UserId);
        }

        var connection = _connectionStorage.Get(x => x.User.Id == request.UserId)[0].connection;

        _connectionStorage.Remove(connection);

        _userMessagesNotifier.UnsubscribeAll(request.UserId);
    }

    private async Task HandleConnection(UserConnection connection, Domain.Entities.Room room)
    {
        while (connection.WebSocket.State == WebSocketState.Open)
        {
            var message = await _socketHandler.ReceiveAsync(connection.WebSocket);

            var type = JsonHelper.GetSocketMessageType(message);

            if (type == null)
            {
                await _socketHelper.SendErrorToClientsAsync(
                    [connection],
                    "Invalid socket message format"
                );
            }

            foreach (var handler in _roomHandlers)
            {
                if (handler.Type == type)
                {
                    await handler.HandleAsync(room, connection, message);
                }
            }

            var userConnection = _connectionStorage
                .Get(room)
                .FirstOrDefault(x => x.WebSocket == connection.WebSocket);

            var socketMsgEnvelope = new SocketMessageEnvelope(userConnection, room, message, type);

            _userMessagesNotifier.PublishMessage(socketMsgEnvelope);
        }

        _userMessagesNotifier.UnsubscribeAll(connection.User.Id);
    }
}

using System.Net.WebSockets;

namespace server.Application.Services.RoomManager.Dtos.Requests.ConnectToRoom;

public record ConnectToRoomRequest(Guid RoomId, Guid UserId, WebSocket Socket);

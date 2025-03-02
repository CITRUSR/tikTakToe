using server.Application.Services.RoomManager.Dtos.Requests;
using server.Application.Services.RoomManager.Dtos.Requests.ConnectToRoom;

namespace server.Application.Contracts.Services;

public interface IRoomManager
{
    Task ConnectToRoomAsync(ConnectToRoomRequest request);
    Task DisconnectFromRoomAsync(DisconnectFromRoomRequest request);
}

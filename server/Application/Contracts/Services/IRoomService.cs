using server.Application.Services.Room.Dtos.Requests.GetRoom;
using server.Application.Services.Room.Dtos.Responses;

namespace server.Application.Contracts.Services;

public interface IRoomService
{
    Task<RoomDto> GetRoomAsync(GetRoomRequest request);
}

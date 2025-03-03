using server.Application.Services.Room.Dtos.Requests.CreateRoom;
using server.Application.Services.Room.Dtos.Requests.GetRoom;
using server.Application.Services.Room.Dtos.Responses;
using server.Domain.Entities;

namespace server.Application.Contracts.Services;

public interface IRoomService
{
    Task<RoomDto> GetRoomAsync(GetRoomRequest request);
    Task<List<Room>> GetAllAsync();
    Task<Room> CreateAsync(CreateRoomRequest request);
}

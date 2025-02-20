using Mapster;
using server.Application.Contracts.Repositories;
using server.Application.Contracts.Services;
using server.Application.Services.Room.Dtos.Requests.GetRoom;
using server.Application.Services.Room.Dtos.Responses;
using server.Domain.Exceptions.Room;

namespace server.Application.Services.Room;

public class RoomService(IUnitOfWork unitOfWork) : IRoomService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<List<Domain.Entities.Room>> GetAllAsync()
    {
        return await _unitOfWork.RoomRepository.GetAllAsync();
    }

    public async Task<RoomDto> GetRoomAsync(GetRoomRequest request)
    {
        var room = await _unitOfWork.RoomRepository.GetAsync(request.Id);

        if (room == null)
        {
            throw new RoomNotFoundException(request.Id);
        }

        return room.Adapt<RoomDto>();
    }
}

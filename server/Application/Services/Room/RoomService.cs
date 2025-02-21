using Mapster;
using server.Application.Contracts.Repositories;
using server.Application.Contracts.Services;
using server.Application.Services.Room.Dtos.Requests.CreateRoom;
using server.Application.Services.Room.Dtos.Requests.GetRoom;
using server.Application.Services.Room.Dtos.Responses;
using server.Domain.Exceptions.Room;
using server.Domain.Exceptions.User;

namespace server.Application.Services.Room;

public class RoomService(IUnitOfWork unitOfWork) : IRoomService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Domain.Entities.Room> CreateAsync(CreateRoomRequest request)
    {
        var owner = await _unitOfWork.UserRepository.GetAsync(request.OwnerId);

        if (owner == null)
        {
            throw new UserNotFoundException(request.OwnerId);
        }

        var room = new Domain.Entities.Room()
        {
            Id = Guid.CreateVersion7(),
            Owner = owner,
            ConnectedUser = null,
        };

        await _unitOfWork.RoomRepository.InsertAsync(room);

        return room;
    }

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

using server.Application.Contracts.Repositories;
using server.Domain.Entities;
using server.Infrastructure.Managers;

namespace server.Infrastructure.Repositories;

public class LocalRoomRepository(IRoomManager roomManager) : IRoomRepository
{
    private readonly IRoomManager _roomManager = roomManager;

    public Task<List<Room>> GetAll()
    {
        return Task.FromResult(_roomManager.GetAll().ToList());
    }

    public Task<Room?> GetAsync(Guid roomId)
    {
        var room = _roomManager.Get(room => room.Id == roomId).FirstOrDefault();

        return Task.FromResult(room);
    }
}

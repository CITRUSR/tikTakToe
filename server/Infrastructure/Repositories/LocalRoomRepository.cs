using server.Application.Contracts.Repositories;
using server.Domain.Entities;
using server.Infrastructure.Storages;

namespace server.Infrastructure.Repositories;

public class LocalRoomRepository(IRoomStorage roomStorage) : IRoomRepository
{
    private readonly IRoomStorage _roomStorage = roomStorage;

    public Task<List<Room>> GetAllAsync()
    {
        return Task.FromResult(_roomStorage.GetAll().ToList());
    }

    public Task<Room?> GetAsync(Guid roomId)
    {
        var room = _roomStorage.Get(room => room.Id == roomId).FirstOrDefault();

        return Task.FromResult(room);
    }

    public Task<Room> InsertAsync(Room room)
    {
        _roomStorage.Add(room);

        return Task.FromResult(room);
    }
}

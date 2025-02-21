using server.Domain.Entities;

namespace server.Infrastructure.Storages;

public interface IRoomStorage
{
    List<Room> GetAll();
    List<Room> Get(Func<Room, bool> predicate);
    Room Add(Room room);
    Room Remove(Room room);
}

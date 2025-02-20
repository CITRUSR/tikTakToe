using server.Domain.Entities;

namespace server.Infrastructure.Managers;

public interface IRoomManager
{
    List<Room> GetAll();
    List<Room> Get(Func<Room, bool> predicate);
    Room Add(Room room);
    Room Remove(Room room);
}

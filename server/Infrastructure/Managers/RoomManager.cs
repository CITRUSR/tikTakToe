using server.Domain.Entities;

namespace server.Infrastructure.Managers;

public class RoomManager : IRoomManager
{
    private readonly List<Room> _rooms = [];
    private readonly Lock _lock = new();

    public Room Add(Room room)
    {
        lock (_lock)
        {
            _rooms.Add(room);
        }

        return room;
    }

    public List<Room> Get(Func<Room, bool> predicate)
    {
        return [.. _rooms.Where(predicate)];
    }

    public List<Room> GetAll()
    {
        return _rooms;
    }

    public Room Remove(Room room)
    {
        lock (_lock)
        {
            _rooms.Remove(room);
        }

        return room;
    }
}

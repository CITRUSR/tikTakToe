using System.Collections.Concurrent;
using server.Application.Contracts.Repositories;
using server.Domain.Entities;
using server.Domain.Exceptions.RoomConnection;

namespace server.Infrastructure.Storages;

public class RoomConnectionStorage : IRoomConnectionStorage
{
    private readonly ConcurrentDictionary<Room, List<UserConnection>> _connections = [];

    public Room Add(Room room, UserConnection connection)
    {
        _connections.AddOrUpdate(
            room,
            [connection],
            (room, connections) =>
            {
                connections.Add(connection);
                return connections;
            }
        );

        return room;
    }

    public List<(Room, UserConnection)> Get(Func<UserConnection, bool> predicate)
    {
        var tuples = _connections
            .SelectMany(x => x.Value, (room, connection) => (room.Key, connection))
            .Where(t => predicate(t.connection))
            .ToList();

        return tuples;
    }

    public List<UserConnection?> Get(Room room)
    {
        return _connections[room];
    }

    public List<UserConnection?> Get(Guid roomId)
    {
        var key = _connections.Keys.FirstOrDefault(x => x.Id == roomId);

        return _connections[key];
    }

    public List<UserConnection> GetAll()
    {
        return _connections.Values.SelectMany(x => x).ToList();
    }

    public Room Remove(UserConnection connection)
    {
        var room = _connections.FirstOrDefault(x => x.Value.Contains(connection)).Key;

        if (room == null)
        {
            throw new RoomConnectionNotFoundException(connection.User.Id);
        }

        _connections[room].Remove(connection);

        return room;
    }
}

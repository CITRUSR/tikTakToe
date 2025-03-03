using server.Domain.Entities;

namespace server.Application.Contracts.Repositories;

public interface IRoomConnectionStorage
{
    List<UserConnection> GetAll();
    List<(Room room, UserConnection connection)> Get(Func<UserConnection, bool> predicate);
    List<UserConnection?> Get(Room room);
    List<UserConnection?> Get(Guid roomId);
    Room Add(Room room, UserConnection connection);
    Room Remove(UserConnection connection);
}

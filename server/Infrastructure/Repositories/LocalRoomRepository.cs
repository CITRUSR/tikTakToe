using server.Application.Contracts.Repositories;
using server.Infrastructure.Managers;

namespace server.Infrastructure.Repositories;

public class LocalRoomRepository(IRoomManager roomManager) : IRoomRepository
{
    private readonly IRoomManager _roomManager = roomManager;
}

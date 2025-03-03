using server.Domain.Entities;

namespace server.Application.Contracts.Repositories;

public interface IRoomRepository
{
    Task<List<Room>> GetAllAsync();
    Task<Room?> GetAsync(Guid roomId);
    Task<Room> InsertAsync(Room room);
}

using server.Domain.Entities;

namespace server.Application.Contracts.Repositories;

public interface IRoomRepository
{
    Task<List<Room>> GetAll();
    Task<Room?> GetAsync(Guid roomId);
}

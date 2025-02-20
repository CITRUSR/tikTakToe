using server.Domain.Entities;

namespace server.Application.Contracts.Repositories;

public interface IRoomRepository
{
    Task<Room?> GetAsync(Guid roomId);
}

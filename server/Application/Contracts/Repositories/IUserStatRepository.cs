using server.Domain.Entities;

namespace server.Application.Contracts.Repositories;

public interface IUserStatRepository
{
    Task<UserStat?> GetAsync(Guid id);
}

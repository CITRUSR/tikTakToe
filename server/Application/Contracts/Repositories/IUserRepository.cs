using server.Domain.Entities;

namespace server.Application.Contracts.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetAsync(Guid id);
    Task<User> GetAsync(string nickname);
    Task InsertAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}

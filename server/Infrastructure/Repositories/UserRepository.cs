using server.Application.Contracts.Repositories;
using server.Domain.Entities;
using server.Infrastructure.Factories;

namespace server.Infrastructure.Repositories;

public class UserRepository(IConnectionFactory connectionFactory) : IUserRepository
{
    private readonly IConnectionFactory _connectionFactory = connectionFactory;

    public Task DeleteAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetAsync(string nickname)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }
}

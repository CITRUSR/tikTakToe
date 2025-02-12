using Dapper;
using server.Application.Contracts.Repositories;
using server.Domain.Entities;
using server.Infrastructure.Factories;
using server.Infrastructure.Sql.Queries;

namespace server.Infrastructure.Repositories;

public class UserRepository(IConnectionFactory connectionFactory) : IUserRepository
{
    private readonly IConnectionFactory _connectionFactory = connectionFactory;

    public Task DeleteAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();

        await connection.OpenAsync();

        var users = await connection.QueryAsync<User>(UserQueries.GetAll);

        return [.. users];
    }

    public async Task<User?> GetAsync(Guid id)
    {
        using var connection = _connectionFactory.CreateConnection();

        await connection.OpenAsync();

        var parameters = new DynamicParameters();

        parameters.Add("@UserId", id);

        var user = await connection.QuerySingleOrDefaultAsync<User>(
            UserQueries.GetById,
            parameters
        );

        return user;
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

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
        var parameters = new DynamicParameters();

        parameters.Add("@UserId", id);

        var user = await GetUserAsync(UserQueries.GetById, parameters);

        return user;
    }

    public async Task<User?> GetAsync(string nickname)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@Nickname", nickname);

        var user = await GetUserAsync(UserQueries.GetByNickname, parameters);

        return user;
    }

    public Task InsertAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    private async Task<User?> GetUserAsync(string query, DynamicParameters parameters)
    {
        using var connection = _connectionFactory.CreateConnection();

        var user = await connection.QuerySingleOrDefaultAsync<User>(query, parameters);

        return user;
    }
}

using System.Data;
using Dapper;
using server.Application.Contracts.Repositories;
using server.Domain.Entities;
using server.Infrastructure.Sql.Queries;

namespace server.Infrastructure.Repositories;

public class UserRepository(IDbConnection connection) : IUserRepository
{
    private readonly IDbConnection _connection = connection;

    public Task DeleteAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> GetAllAsync()
    {
        var users = await _connection.QueryAsync<User>(UserQueries.GetAll);

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

    public async Task InsertAsync(User user)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Nickname", user.Nickname);
        parameters.Add("@Password", user.Password);
        parameters.Add("@Id", user.Id);

        await _connection.ExecuteAsync(UserQueries.Insert, parameters);
    }

    public async Task<User?> UpdateAsync(User user)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Nickname", user.Nickname);
        parameters.Add("@Password", user.Password);
        parameters.Add("@Id", user.Id);

        var updatedUser = await _connection.QuerySingleOrDefaultAsync<User>(
            UserQueries.Update,
            parameters
        );

        return updatedUser;
    }

    private async Task<User?> GetUserAsync(string query, DynamicParameters parameters)
    {
        var user = await _connection.QuerySingleOrDefaultAsync<User>(query, parameters);

        return user;
    }
}

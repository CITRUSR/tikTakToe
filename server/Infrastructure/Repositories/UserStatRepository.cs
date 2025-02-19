using Dapper;
using server.Application.Contracts.Repositories;
using server.Domain.Entities;
using server.Infrastructure.Factories;
using server.Infrastructure.Sql.Queries;

namespace server.Infrastructure.Repositories;

public class UserStatRepository(IConnectionFactory connectionFactory) : IUserStatRepository
{
    private readonly IConnectionFactory _connectionFactory = connectionFactory;

    public async Task<UserStat?> GetAsync(Guid id)
    {
        using var connection = _connectionFactory.CreateConnection();

        var parameters = new DynamicParameters();
        parameters.Add("@UserId", id);

        var stat = await connection.QueryAsync<UserStat, User, UserStat>(
            UserStatQueries.Get,
            (stat, user) =>
            {
                stat.User = user;
                return stat;
            },
            parameters
        );

        return stat.FirstOrDefault();
    }

    public async Task<UserStat?> UpdateAsync(UserStat stat)
    {
        using var connection = _connectionFactory.CreateConnection();

        var parameters = new DynamicParameters();

        parameters.Add("@UserId", stat.User.Id);
        parameters.Add("@Wins", stat.Wins);
        parameters.Add("@Losses", stat.Losses);
        parameters.Add("@GamesCount", stat.GamesCount);

        var userStat = await connection.QueryAsync<UserStat, User, UserStat>(
            UserStatQueries.Update,
            (stat, user) =>
            {
                stat.User = user;
                return stat;
            },
            parameters
        );

        return userStat.FirstOrDefault();
    }
}

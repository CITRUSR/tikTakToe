using System.Data;
using Dapper;
using server.Application.Contracts.Repositories;
using server.Domain.Entities;
using server.Infrastructure.Sql.Queries;

namespace server.Infrastructure.Repositories;

public class UserStatRepository(IDbConnection connection) : IUserStatRepository
{
    private readonly IDbConnection _connection = connection;

    public async Task<UserStat?> GetAsync(Guid id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", id);

        var stat = await _connection.QueryAsync<UserStat, User, UserStat>(
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
        var parameters = new DynamicParameters();

        parameters.Add("@UserId", stat.User.Id);
        parameters.Add("@Wins", stat.Wins);
        parameters.Add("@Losses", stat.Losses);
        parameters.Add("@GamesCount", stat.GamesCount);

        var userStat = await _connection.QueryAsync<UserStat, User, UserStat>(
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

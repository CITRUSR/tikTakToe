using Dapper;
using server.Application.Contracts.Repositories;
using server.Domain.Entities;
using server.Infrastructure.Factories;
using server.Infrastructure.Sql.Queries;

namespace server.Infrastructure.Repositories;

public class RefreshTokenRepository(IConnectionFactory connectionFactory) : IRefreshTokenRepository
{
    private readonly IConnectionFactory _connectionFactory = connectionFactory;

    public async Task<RefreshToken> InsertAsync(RefreshToken token)
    {
        using var connection = _connectionFactory.CreateConnection();

        var parameters = new DynamicParameters();
        parameters.Add("@UserId", token.UserId);
        parameters.Add("@Token", token.Token);
        parameters.Add("@ExpiresAt", token.ExpiresAt);

        var insertedToken = await connection.QuerySingleOrDefaultAsync<RefreshToken>(
            RefreshTokenQueries.Insert,
            parameters
        );

        return insertedToken;
    }
}

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
        var insertedToken = await SetTokenAsync(token, RefreshTokenQueries.Insert);

        return insertedToken;
    }

    public async Task<RefreshToken?> UpdateAsync(RefreshToken token)
    {
        var updatedToken = await SetTokenAsync(token, RefreshTokenQueries.Update);

        return updatedToken;
    }

    private async Task<RefreshToken?> SetTokenAsync(RefreshToken token, string query)
    {
        using var connection = _connectionFactory.CreateConnection();

        var parameters = new DynamicParameters();
        parameters.Add("@UserId", token.UserId);
        parameters.Add("@Token", token.Token);
        parameters.Add("@ExpiresAt", token.ExpiresAt);

        var settedToken = await connection.QuerySingleOrDefaultAsync<RefreshToken>(
            query,
            parameters
        );

        return settedToken;
    }
}

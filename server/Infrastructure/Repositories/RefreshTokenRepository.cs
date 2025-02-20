using System.Data;
using Dapper;
using server.Application.Contracts.Repositories;
using server.Domain.Entities;
using server.Infrastructure.Sql.Queries;

namespace server.Infrastructure.Repositories;

public class RefreshTokenRepository(IDbConnection connection) : IRefreshTokenRepository
{
    private readonly IDbConnection _connection = connection;

    public async Task<RefreshToken?> GetAsync(Guid userId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", userId);

        var refreshToken = await _connection.QuerySingleOrDefaultAsync<RefreshToken>(
            RefreshTokenQueries.GetByUserId,
            parameters
        );

        return refreshToken;
    }

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
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", token.UserId);
        parameters.Add("@Token", token.Token);
        parameters.Add("@ExpiresAt", token.ExpiresAt);

        var settedToken = await _connection.QuerySingleOrDefaultAsync<RefreshToken>(
            query,
            parameters
        );

        return settedToken;
    }
}

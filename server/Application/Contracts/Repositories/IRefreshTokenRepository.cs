using server.Domain.Entities;

namespace server.Application.Contracts.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> InsertAsync(RefreshToken token);
}

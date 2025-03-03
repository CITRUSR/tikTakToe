using server.Domain.Entities;

namespace server.Application.Contracts.Providers;

public interface ITokenProvider
{
    string GenerateAccessToken(User user);
    RefreshToken GenerateRefreshToken(Guid userId);
}

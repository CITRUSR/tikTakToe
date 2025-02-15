using server.Application.Contracts.Providers;
using server.Application.Contracts.Repositories;
using server.Application.Contracts.Services;
using server.Application.Services.Auth.Dtos.Responses;

namespace server.Application.Services.Auth;

public class AuthService(ITokenProvider tokenProvider, IUnitOfWork unitOfWork) : IAuthService
{
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<AuthResponse> RegisterAsync(RegisterUserRequest request)
    {
        var user = new Domain.Entities.User()
        {
            Id = Guid.CreateVersion7(),
            Nickname = request.Nickname,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
        };

        var refreshToken = _tokenProvider.GenerateRefreshToken(user.Id);

        var accessToken = _tokenProvider.GenerateAccessToken(user);

        try
        {
            _unitOfWork.BeginTransaction();

            await _unitOfWork.UserRepository.InsertAsync(user);
            await _unitOfWork.RefreshTokenRepository.InsertAsync(refreshToken);

            _unitOfWork.CommitTransaction();
        }
        catch
        {
            _unitOfWork.RollbackTransaction();
            throw;
        }

        return new AuthResponse(accessToken, refreshToken.Token);
    }
}

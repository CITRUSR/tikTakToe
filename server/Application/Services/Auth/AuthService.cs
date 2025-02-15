using server.Application.Common.Exceptions.Auth;
using server.Application.Contracts.Providers;
using server.Application.Contracts.Repositories;
using server.Application.Contracts.Services;
using server.Application.Services.Auth.Dtos.Requests.LoginUser;
using server.Application.Services.Auth.Dtos.Responses;

namespace server.Application.Services.Auth;

public class AuthService(ITokenProvider tokenProvider, IUnitOfWork unitOfWork) : IAuthService
{
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<AuthResponse> LoginAsync(LoginUserRequest request)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(request.Nickname);

        if (user == null)
        {
            throw new IdentityException("Invalid nickname or password");
        }

        try
        {
            BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
        }
        catch
        {
            throw new IdentityException("Invalid nickname or password");
        }

        var refreshToken = _tokenProvider.GenerateRefreshToken(user.Id);

        await _unitOfWork.RefreshTokenRepository.UpdateAsync(refreshToken);

        var accessToken = _tokenProvider.GenerateAccessToken(user);

        return new AuthResponse(accessToken, refreshToken.Token);
    }

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

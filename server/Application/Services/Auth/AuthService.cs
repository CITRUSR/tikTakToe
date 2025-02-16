using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using server.Application.Common.Exceptions.Auth;
using server.Application.Contracts.Providers;
using server.Application.Contracts.Repositories;
using server.Application.Contracts.Services;
using server.Application.Options;
using server.Application.Services.Auth.Dtos.Requests.LoginUser;
using server.Application.Services.Auth.Dtos.Requests.RefreshUserToken;
using server.Application.Services.Auth.Dtos.Responses;
using server.Domain.Exceptions.RefreshToken;
using server.Domain.Exceptions.User;

namespace server.Application.Services.Auth;

public class AuthService(
    ITokenProvider tokenProvider,
    IUnitOfWork unitOfWork,
    IAuthOptions authOptions
) : IAuthService
{
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthOptions _authOptions = authOptions;

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

    public async Task<AuthResponse> RefreshAsync(RefreshUserTokenRequest request)
    {
        var parameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidAudience = _authOptions.Audience,
            ValidateIssuer = true,
            ValidIssuer = _authOptions.Issuer,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _authOptions.GetSymmetricSecurityKey(),
            ValidateLifetime = false,
        };

        var validator = new JwtSecurityTokenHandler();

        var validationResult = await validator.ValidateTokenAsync(request.AccessToken, parameters);

        if (validationResult.Exception != null)
        {
            throw new InvalidAccessTokenSignatureException();
        }

        var idClaim = validationResult.ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        if (idClaim == null)
        {
            throw new InvalidAccessTokenException(
                "Invalid access token, missing name identifier claim"
            );
        }

        var userId = Guid.Parse(idClaim.Value);

        var refreshToken = await _unitOfWork.RefreshTokenRepository.GetAsync(userId);

        if (
            refreshToken == null
            || refreshToken.Token != request.RefreshToken
            || refreshToken.ExpiresAt > DateTime.Now
        )
        {
            throw new InvalidRefreshTokenException();
        }

        var user = await _unitOfWork.UserRepository.GetAsync(userId);

        if (user == null)
        {
            throw new UserNotFoundException(userId);
        }

        var newAccessToken = _tokenProvider.GenerateAccessToken(user);
        var newRefreshToken = _tokenProvider.GenerateRefreshToken(user.Id);

        await _unitOfWork.RefreshTokenRepository.UpdateAsync(newRefreshToken);

        return new AuthResponse(newAccessToken, newRefreshToken.Token);
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

using server.Application.Services.Auth.Dtos.Requests.LoginUser;
using server.Application.Services.Auth.Dtos.Requests.RefreshUserToken;
using server.Application.Services.Auth.Dtos.Responses;

namespace server.Application.Contracts.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterUserRequest request);
    Task<AuthResponse> LoginAsync(LoginUserRequest request);
    Task<AuthResponse> RefreshAsync(RefreshUserTokenRequest request);
}

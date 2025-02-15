using server.Application.Services.Auth.Dtos.Responses;

namespace server.Application.Contracts.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterUserRequest request);
}

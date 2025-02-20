namespace server.Application.Services.Auth.Dtos.Responses;

public record AuthResponse(string AccessToken, string RefreshToken);

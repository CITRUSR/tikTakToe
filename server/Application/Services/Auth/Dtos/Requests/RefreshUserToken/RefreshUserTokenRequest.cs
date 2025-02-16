namespace server.Application.Services.Auth.Dtos.Requests.RefreshUserToken;

public record RefreshUserTokenRequest(string RefreshToken, string AccessToken);

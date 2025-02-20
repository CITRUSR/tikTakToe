namespace server.Application.Services.Auth.Dtos.Responses;

public record RegisterUserRequest(string Nickname, string Password, string ConfirmPassword);

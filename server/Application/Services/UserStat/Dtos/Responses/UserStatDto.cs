namespace server.Application.Services.UserStat.Dtos.Responses;

public record UserStatDto(Guid UserId, int Wins, int Losses, int GamesCount);

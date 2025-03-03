namespace server.Application.Services.UserStat.Dtos.Requests.UpdateUserStat;

public record UpdateUserStatRequest(Guid UserId, int Wins, int Losses, int GamesCount);

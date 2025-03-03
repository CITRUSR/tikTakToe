namespace server.Application.Services.GameSession.Dtos;

public class GameResult
{
    public bool GameInProcess => !IsDraw && WinnerId == null;
    public bool IsDraw { get; set; }
    public Guid? WinnerId { get; set; }
}

namespace server.Application.Contracts.Services;

public interface IGameSessionOrchestrator
{
    Task HandleGameAsync(Guid gameSessionId);
}

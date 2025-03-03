namespace server.Application.Contracts.Providers;

public interface IGameSessionCancellationTokenProvider
{
    CancellationToken GetCancellationToken(Guid gameId);
    Task CancelAsync(Guid gameId);
}

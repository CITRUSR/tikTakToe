using System.Collections.Concurrent;
using server.Application.Contracts.Providers;

namespace server.Application.Providers;

public class GameSessionCancellationTokenProvider : IGameSessionCancellationTokenProvider
{
    private readonly ConcurrentDictionary<Guid, CancellationTokenSource> _cancellationTokens = [];

    public async Task CancelAsync(Guid gameId)
    {
        _cancellationTokens.TryGetValue(gameId, out var source);
        if (source != null)
            await source.CancelAsync();
    }

    public CancellationToken GetCancellationToken(Guid gameId)
    {
        _cancellationTokens.GetOrAdd(gameId, new CancellationTokenSource());

        return _cancellationTokens[gameId].Token;
    }
}

using System.Collections.Concurrent;
using server.Application.Contracts.Repositories;
using server.Application.Extensions;
using server.Application.Services.GameSession.Dtos;

namespace server.Infrastructure.Repositories;

public class LocalGameSessionRepository : IGameSessionRepository
{
    private readonly ConcurrentDictionary<Guid, GameSessionDto> _gameSessions = [];

    public Task<GameSessionDto?> DeleteAsync(Guid gameId)
    {
        _gameSessions.TryRemove(gameId, out GameSessionDto gameSession);

        return Task.FromResult(gameSession.DeepCopy());
    }

    public Task<List<GameSessionDto>> GetAllAsync()
    {
        return Task.FromResult(_gameSessions.Values.Select(x => x.DeepCopy()).ToList());
    }

    public Task<GameSessionDto?> GetAsync(Guid gameId)
    {
        _gameSessions.TryGetValue(gameId, out var gameSession);

        return Task.FromResult(gameSession.DeepCopy());
    }

    public Task<GameSessionDto?> InsertAsync(GameSessionDto gameSession)
    {
        var res = _gameSessions.TryAdd(gameSession.Id, gameSession);

        return Task.FromResult(res ? gameSession.DeepCopy() : null);
    }

    public async Task<GameSessionDto?> UpdateAsync(GameSessionDto gameSession)
    {
        var prevGameSession = await GetAsync(gameSession.Id);

        if (prevGameSession == null)
        {
            return null;
        }

        prevGameSession.GameInProcess = gameSession.GameInProcess;
        prevGameSession.Map = gameSession.Map;
        if (gameSession.Winner != null)
        {
            prevGameSession.Winner = gameSession.Winner;
        }
        prevGameSession.ActivePlayerConnection = gameSession.ActivePlayerConnection;
        prevGameSession.PlayerConnections = gameSession.PlayerConnections;
        prevGameSession.EndTime = gameSession.EndTime;
        prevGameSession.StartTime = gameSession.StartTime;

        var res = _gameSessions.TryUpdate(
            gameSession.Id,
            prevGameSession,
            _gameSessions[gameSession.Id]
        );

        return res ? prevGameSession.DeepCopy() : null;
    }
}

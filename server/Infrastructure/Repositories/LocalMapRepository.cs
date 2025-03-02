using System.Collections.Concurrent;
using server.Application.Contracts.Repositories;
using server.Application.Extensions;
using server.Application.Services.Map.Dtos;

namespace server.Infrastructure.Repositories;

public class LocalMapRepository : IMapRepository
{
    private readonly ConcurrentDictionary<Guid, GameBoard> _gameMaps = [];

    public Task DeleteAsync(GameBoard gameBoard)
    {
        _gameMaps.TryRemove(gameBoard.GameId, out _);
        return Task.CompletedTask;
    }

    public Task<List<GameBoard>> GetAllAsync()
    {
        return Task.FromResult(_gameMaps.Values.Select(x => x.DeepCopy()).ToList());
    }

    public Task<GameBoard?> GetAsync(Guid gameId)
    {
        var res = _gameMaps.TryGetValue(gameId, out var gameBoard);

        return Task.FromResult(res ? gameBoard.DeepCopy() : null);
    }

    public Task<GameBoard?> InsertAsync(GameBoard gameBoard)
    {
        _gameMaps.TryAdd(gameBoard.GameId, gameBoard.DeepCopy());
        return Task.FromResult<GameBoard?>(gameBoard);
    }

    public Task<GameBoard?> UpdateAsync(GameBoard gameBoard)
    {
        var gameBoardCopy = gameBoard.DeepCopy();

        _gameMaps.AddOrUpdate(
            gameBoard.GameId,
            gameBoardCopy,
            (key, existingGameBoard) => gameBoardCopy
        );

        return Task.FromResult<GameBoard?>(gameBoard);
    }
}

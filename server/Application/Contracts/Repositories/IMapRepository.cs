using server.Application.Services.Map.Dtos;

namespace server.Application.Contracts.Repositories;

public interface IMapRepository
{
    Task<GameBoard?> GetAsync(Guid gameId);
    Task<List<GameBoard>> GetAllAsync();
    Task<GameBoard?> InsertAsync(GameBoard gameBoard);
    Task<GameBoard?> UpdateAsync(GameBoard gameBoard);
    Task DeleteAsync(GameBoard gameBoard);
}

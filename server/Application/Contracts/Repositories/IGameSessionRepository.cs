using server.Application.Services.GameSession.Dtos;

namespace server.Application.Contracts.Repositories;

public interface IGameSessionRepository
{
    Task<GameSessionDto?> GetAsync(Guid gameId);
    Task<List<GameSessionDto>> GetAllAsync();
    Task<GameSessionDto?> InsertAsync(GameSessionDto gameSession);
    Task<GameSessionDto?> UpdateAsync(GameSessionDto gameSession);
    Task<GameSessionDto?> DeleteAsync(Guid gameId);
}

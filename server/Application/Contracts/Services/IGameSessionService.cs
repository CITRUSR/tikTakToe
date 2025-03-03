using server.Application.Services.GameSession.Dtos;
using server.Application.Services.GameSession.Dtos.Requests.CreateGameSession;
using server.Application.Services.GameSession.Dtos.Requests.DeleteGameSession;
using server.Application.Services.GameSession.Dtos.Requests.GetGameSession;
using server.Application.Services.GameSession.Dtos.Requests.UpdateGameSession;

namespace server.Application.Contracts.Services;

public interface IGameSessionService
{
    Task<GameSessionDto> GetAsync(GetGameSessionRequest request);
    Task<List<GameSessionDto>> GetAllAsync();
    Task<GameSessionDto> InsertAsync(CreateGameSessionRequest request);
    Task<GameSessionDto> UpdateAsync(UpdateGameSessionRequest request);
    Task<GameSessionDto> DeleteAsync(DeleteGameSessionRequest request);
}

using server.Application.Services.Map.Dtos;
using server.Application.Services.Map.Dtos.Requests.CreateMap;
using server.Application.Services.Map.Dtos.Requests.DeleteMap;
using server.Application.Services.Map.Dtos.Requests.GetMap;

namespace server.Application.Contracts.Services;

public interface IMapService
{
    Task<GameBoard> GetAsync(GetMapRequest request);

    Task<List<GameBoard>> GetAllAsync();

    Task<GameBoard> InsertAsync(CreateMapRequest request);

    Task<GameBoard> UpdateAsync(GameBoard gameBoard);

    Task DeleteAsync(DeleteMapRequest request);
}

using server.Application.Common.Exceptions.Map;
using server.Application.Contracts.Repositories;
using server.Application.Contracts.Services;
using server.Application.Services.Map.Dtos;
using server.Application.Services.Map.Dtos.Requests.CreateMap;
using server.Application.Services.Map.Dtos.Requests.DeleteMap;
using server.Application.Services.Map.Dtos.Requests.GetMap;

namespace server.Application.Services.Map;

public class MapService(IUnitOfWork unitOfWork) : IMapService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task DeleteAsync(DeleteMapRequest request)
    {
        var map = await GetAsync(new GetMapRequest(request.GameId));

        await _unitOfWork.MapRepository.DeleteAsync(map);
    }

    public async Task<List<GameBoard>> GetAllAsync()
    {
        return await _unitOfWork.MapRepository.GetAllAsync();
    }

    public async Task<GameBoard> GetAsync(GetMapRequest request)
    {
        var map = await _unitOfWork.MapRepository.GetAsync(request.GameId);

        if (map == null)
        {
            throw new MapNotFoundException(request.GameId);
        }

        return map;
    }

    public async Task<GameBoard> InsertAsync(CreateMapRequest request)
    {
        var map = new GameBoard() { GameId = request.GameId };

        var insertedMap = await _unitOfWork.MapRepository.InsertAsync(map);

        return insertedMap;
    }

    public async Task<GameBoard> UpdateAsync(GameBoard gameBoard)
    {
        return await _unitOfWork.MapRepository.UpdateAsync(gameBoard);
    }
}

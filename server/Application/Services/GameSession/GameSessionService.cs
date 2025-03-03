using Mapster;
using server.Application.Common.Exceptions.GameSession;
using server.Application.Contracts.Repositories;
using server.Application.Contracts.Services;
using server.Application.Services.GameSession.Dtos;
using server.Application.Services.GameSession.Dtos.Requests.CreateGameSession;
using server.Application.Services.GameSession.Dtos.Requests.DeleteGameSession;
using server.Application.Services.GameSession.Dtos.Requests.GetGameSession;
using server.Application.Services.GameSession.Dtos.Requests.UpdateGameSession;
using server.Application.Services.Map.Dtos.Requests.DeleteMap;
using server.Application.Services.Map.Dtos.Requests.GetMap;
using server.Application.Services.User.Dtos.Requests.GetUserById;
using server.Application.Services.User.Dtos.Responses;

namespace server.Application.Services.GameSession;

public class GameSessionService(
    IUnitOfWork unitOfWork,
    IUserService userService,
    IMapService mapService
) : IGameSessionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserService _userService = userService;
    private readonly IMapService _mapService = mapService;

    public async Task<GameSessionDto> DeleteAsync(DeleteGameSessionRequest request)
    {
        var deletedEntity = await _unitOfWork.GameSessionRepository.DeleteAsync(request.GameId);

        if (deletedEntity == null)
        {
            throw new GameSessionNotFoundException(request.GameId);
        }

        await _mapService.DeleteAsync(new DeleteMapRequest(request.GameId));

        return deletedEntity;
    }

    public async Task<List<GameSessionDto>> GetAllAsync()
    {
        return await _unitOfWork.GameSessionRepository.GetAllAsync();
    }

    public async Task<GameSessionDto> GetAsync(GetGameSessionRequest request)
    {
        var gameSession = await _unitOfWork.GameSessionRepository.GetAsync(request.GameId);

        if (gameSession == null)
        {
            throw new GameSessionNotFoundException(request.GameId);
        }

        var map = await _mapService.GetAsync(new GetMapRequest(request.GameId));

        gameSession.Map = map;

        return gameSession;
    }

    public async Task<GameSessionDto> InsertAsync(CreateGameSessionRequest request)
    {
        var r = new Random();

        var gameSession = new GameSessionDto()
        {
            Id = Guid.CreateVersion7(),
            StartTime = DateTime.Now.TimeOfDay,
            ActivePlayerConnection = request.Players[r.Next(0, request.Players.Count)],
            PlayerConnections = request.Players,
            GameInProcess = true,
        };

        var insertedSession = await _unitOfWork.GameSessionRepository.InsertAsync(gameSession);

        return insertedSession;
    }

    public async Task<GameSessionDto> UpdateAsync(UpdateGameSessionRequest request)
    {
        var session = await GetAsync(new GetGameSessionRequest(request.GameId));

        if (session.ActivePlayerConnection.User.Id == request.ActivePlayerConnection.User.Id)
        {
            throw new SamePlayerNextTurnException();
        }

        if (request.WinnerId.HasValue)
        {
            var winner = await _userService.GetUserAsync(
                new GetUserByIdRequest((Guid)request.WinnerId)
            );

            session.Winner = winner.Adapt<UserViewModel>();
        }

        session.ActivePlayerConnection = request.ActivePlayerConnection;

        session.EndTime = request.EndTime;

        return await _unitOfWork.GameSessionRepository.UpdateAsync(session);
    }
}

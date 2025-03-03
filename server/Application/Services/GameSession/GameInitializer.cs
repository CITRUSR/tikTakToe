using System.Text.Json;
using Mapster;
using server.Application.Contracts.Services;
using server.Application.Services.GameSession.Dtos.Requests.CreateGameSession;
using server.Application.Services.GameSession.Dtos.SocketMessages.Server;
using server.Application.Services.Map.Dtos;
using server.Application.Services.Map.Dtos.Requests.CreateMap;
using server.Domain.Entities;

namespace server.Application.Services.GameSession;

public class GameInitializer(
    IGameSessionSocketHelper socketHelper,
    IGameSessionService gameSessionService,
    IMapService mapService
) : IGameInitializer
{
    private readonly IGameSessionSocketHelper _socketHelper = socketHelper;
    private readonly IGameSessionService _gameSessionService = gameSessionService;
    private readonly IMapService _mapService = mapService;

    public async Task<Guid> BeginGameAsync(List<UserConnection> userConnections)
    {
        var game = await _gameSessionService.InsertAsync(
            new CreateGameSessionRequest(userConnections)
        );

        var map = await _mapService.InsertAsync(new CreateMapRequest(game.Id));

        var initMsg = new InitSocketMessage()
        {
            ActiveUser = game.ActivePlayerConnection.User,
            Map = map.Adapt<GameBoardDto>(),
        };

        var msg = JsonSerializer.Serialize(initMsg);

        await _socketHelper.SendToAllClientsAsync(userConnections, msg);

        return game.Id;
    }
}

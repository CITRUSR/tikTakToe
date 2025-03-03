using Mapster;
using server.Application.Extensions;
using server.Application.Services.GameSession.Dtos;
using server.Application.Services.Map.Dtos;
using server.Domain.Entities;

namespace server.Application;

public static class ApplicationMapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<GameBoard, GameBoardDto>
            .NewConfig()
            .Map(dest => dest.Map, src => src.GetJaggedArray());
        TypeAdapterConfig<GameSessionDto, Game>
            .NewConfig()
            .Map(dest => dest.Players, src => src.PlayerConnections.Select(x => x.User).ToList());
        TypeAdapterConfig<GameBoardDto, GameBoard>
            .NewConfig()
            .Map(dest => dest, src => new GameBoard(src.Map));
    }
}
